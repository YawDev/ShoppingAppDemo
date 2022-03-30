using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Shopper.App.Models;
using ShoppingDemo.App.Data.Entites;
using ShoppingDemo.App.Mapping;
using ShoppingDemo.EFCore;

namespace ShoppingDemo.App.Services
{
    public interface IShoppingCartService
    {
        ShoppingCartModel MapShoppingCartModel(ApplicationUser user);

        ShoppingCart GetShoppingCart(ApplicationUser user);

        void RemoveCartItem(ShoppingCart cart,ItemModel model);

        void AddCartItem(ShoppingCart cart, ItemModel model);
    }

    public class ShoppingCartService : IShoppingCartService
    {

        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IItemService _itemService;

        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        IMapper _mapper;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IShoppingCartItemRepository shoppingCartItemRepository,
        IItemService itemService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _itemService = itemService;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<EntityToQueryDtoMapper>()).CreateMapper();
        }

        public ShoppingCartModel MapShoppingCartModel(ApplicationUser user)
         {
            var cart = GetShoppingCart(user);
            var model = _mapper.Map<ShoppingCartModel>(cart);
            model.Total = cart.Items.Sum(x => x.ItemListing.Price* x.QuantityInCart);
            return model;
         }

        public ShoppingCart GetShoppingCart(ApplicationUser user)
        {
            var cart = _shoppingCartRepository.GetByUserId(user.Id);
                if(cart is null)
                {
                    cart = new ShoppingCart();
                    cart.Items = new List<ShoppingCartItem>();
                    cart.User = user;
                    _shoppingCartRepository.Add(cart);
                    _shoppingCartRepository.Commit();
                }
                return cart;
        }

        public void RemoveCartItem(ShoppingCart cart,ItemModel model)
        {

            if(cart.Items.Any(x => x.ItemListing?.Id == model.Id && x.QuantityInCart > 1))
            {
                var existingItem = cart.Items.FirstOrDefault(x => x.ItemListing.Id == model.Id);
                existingItem.QuantityInCart--;
                _shoppingCartRepository.Commit();
                return;
            }
            var item = _shoppingCartItemRepository.GetByItemId(model.Id);

            cart.Items.Remove(item);
            _shoppingCartItemRepository.Delete(item);
            _shoppingCartItemRepository.Commit();
        }

        public void AddCartItem(ShoppingCart cart, ItemModel model)
        {
            if(cart.Items.Any(x => x.ItemListing?.Id == model.Id))
            {
                var existingItem = cart.Items.FirstOrDefault(x => x.ItemListing.Id == model.Id);
                existingItem.QuantityInCart++;
                cart.Total = cart.Items.Sum(x => x.ItemListing.Price* x.QuantityInCart);
                _shoppingCartRepository.Commit();
                return;
            }
            var item = _itemService.FindItem(model.Id);
            var shoppingCartItem = new ShoppingCartItem
            {
                Cart = cart,
                ItemListing = item,
                QuantityInCart = 1
            };
            cart.Items.Add(shoppingCartItem);
            cart.Total = cart.Items.Sum(x => x.ItemListing.Price* x.QuantityInCart);
            _shoppingCartRepository.Commit();
        }
    }
}