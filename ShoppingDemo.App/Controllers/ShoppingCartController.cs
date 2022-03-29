using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shopper.App.Models;
using ShoppingDemo.App.Data.Entites;
using ShoppingDemo.App.Mapping;
using ShoppingDemo.App.Services;
using ShoppingDemo.EFCore;

namespace ShoppingDemo.App.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;

        ICustomerComposition _customerComposition;

        IMapper _mapper;

        public ShoppingCartController(ILogger<ShoppingCartController> logger, SignInManager<ApplicationUser> signInManager, IShoppingCartRepository shoppingCartRepository,
        UserManager<ApplicationUser> userManger,IItemRepository itemRepository,IShoppingCartItemRepository shoppingCartItemRepository)
        {
            _logger = logger;
            _signInManager = signInManager;
            _shoppingCartRepository = shoppingCartRepository;
            _userManager = userManger;
            _itemRepository = itemRepository;
             _shoppingCartItemRepository = shoppingCartItemRepository;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<EntityToQueryDtoMapper>()).CreateMapper();
        }

        public IActionResult ViewCart()
        {
            if(_signInManager.IsSignedIn(User))
            {
                var user = _userManager.GetUserAsync(User).Result;
                return View(_customerComposition.GetShoppingCart(user));
            }
            return RedirectToAction("Login","Identity");
        }

        public IActionResult AddToCart(Guid Id)
        {
            if(_signInManager.IsSignedIn(User))
            {
                var user = _userManager.GetUserAsync(User).Result;
                var cart = _shoppingCartRepository.GetByUserId(user.Id);
                if(cart is null)
                {
                    cart = new ShoppingCart();
                    _shoppingCartRepository.Add(cart);
                    cart.User = user;
                    _shoppingCartRepository.Commit();
                }

                var item = _itemRepository.GetById(Id);
                return View(_mapper.Map<ItemModel>(item));
                
            }
            return RedirectToAction("Login","Identity");        
        }

        [HttpPost]
        public IActionResult AddToCart(ItemModel model)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var cart = _shoppingCartRepository.GetByUserId(user.Id);

            if(cart.Items.Any(x => x.ItemListing?.Id == model.Id))
            {
                var existingItem = cart.Items.FirstOrDefault(x => x.ItemListing.Id == model.Id);
                existingItem.QuantityInCart++;
                cart.Total = cart.Items.Sum(x => x.ItemListing.Price* x.QuantityInCart);
                _shoppingCartRepository.Commit();
                return RedirectToAction("ViewCart");
            }
            var item = _itemRepository.GetById(model.Id);
            var shoppingCartItem = new ShoppingCartItem
            {
                Cart = cart,
                ItemListing = item,
                QuantityInCart = 1
            };
            cart.Items.Add(shoppingCartItem);
            cart.Total = cart.Items.Sum(x => x.ItemListing.Price* x.QuantityInCart);
            _shoppingCartRepository.Commit();
            return RedirectToAction("ViewCart");
        }




        public IActionResult RemoveFromCart(Guid Id)
        {
            if(_signInManager.IsSignedIn(User))
            {
                var user = _userManager.GetUserAsync(User).Result;
                var cart = _shoppingCartRepository.GetByUserId(user.Id);

                if(cart is null)
                {
                    cart = new ShoppingCart();
                    _shoppingCartRepository.Add(cart);
                    _shoppingCartRepository.Commit();
                }

                var item = _itemRepository.GetById(Id);
                return View(_mapper.Map<ItemModel>(item));
                
            }
            return RedirectToAction("Login","Identity");        
        }
        [HttpPost]
        public IActionResult RemoveFromCart(ItemModel model)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var cart = _shoppingCartRepository.GetByUserId(user.Id);

            if(cart.Items.Any(x => x.ItemListing?.Id == model.Id && x.QuantityInCart > 1))
            {
                var existingItem = cart.Items.FirstOrDefault(x => x.ItemListing.Id == model.Id);
                existingItem.QuantityInCart--;
                _shoppingCartRepository.Commit();
                return RedirectToAction("ViewCart");
            }
            var item = _shoppingCartItemRepository.GetByItemId(model.Id);

            cart.Items.Remove(item);
            _shoppingCartItemRepository.Delete(item);
            _shoppingCartItemRepository.Commit();
            return RedirectToAction("ViewCart");
        }
    }
}