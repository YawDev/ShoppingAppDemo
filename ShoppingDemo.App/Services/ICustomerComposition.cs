using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Shopper.App.Models;
using ShoppingDemo.App.Data.Entites;
using ShoppingDemo.App.Mapping;
using ShoppingDemo.EFCore;

namespace ShoppingDemo.App.Services
{
    public interface ICustomerComposition
    {
         void ProcessOrder(PlaceOrderModel model,Order order, ApplicationUser user);

         string GetErrorMessage();

         void SaveOrder(PlaceOrderModel model, Order order,string userId);

         PlaceOrderModel MapOrderForm(string userid);

         List<OrderModel> GetOrders(string userId);

         OrderModel GetOrderDetails(Guid Id);

         Dictionary<string,string> GetModelErrors();

         ShoppingCartModel GetShoppingCart(ApplicationUser user);
   
    }

    public class CustomerComposition : ICustomerComposition
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICryptoService _cryptoService;
        private readonly IOrderService _orderService;
        IMapper _mapper;

        public CustomerComposition(IOrderRepository orderRepository, IUserRepository userRepository, IShoppingCartRepository shoppingCartRepository, ICryptoService cryptoService, IOrderService orderService)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _cryptoService = cryptoService;
            _orderService = orderService;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<EntityToQueryDtoMapper>()).CreateMapper();
        }

        public PlaceOrderModel MapOrderForm(string userid)
        {
            var cart = _shoppingCartRepository.GetByUserId(userid);
            var order = _orderService.PrepareOrder(_mapper.Map<ShoppingCartModel>(cart));
            var orderModel = _mapper.Map<PlaceOrderModel>(order);
            return orderModel;
        }

        public string GetErrorMessage()
        {
            var errors = GetModelErrors();
            if(errors.Count > 0)
            {
                var sb = new StringBuilder();
                foreach(var error in errors)
                {
                    sb.AppendLine(error.Value+",");
                    sb.AppendLine("\n");
                }
                return sb.ToString();
            }
            return null;
        }

        public Dictionary<string,string> GetModelErrors()
        {
            return _orderService.GetErrors();
        }

        public void ProcessOrder(PlaceOrderModel model, Order order,ApplicationUser user)
        {


            if(model.UseExistingContactInfo)
                _orderService.UseExistingContactInfo(user, order, model);
            else
                _orderService.NoContactProvided(model);


                if(model.Payment.UseExistingCard)
                {
                    var card = _userRepository.GetPaymentDetails(user.Id);


                    
                    var billingAddress = _userRepository.GetBillingAddress(user.Id);

                    if(!_orderService.MissingCardBilling(billingAddress, card))
                    {   
                        _orderService.UseExistingCard(card,order);
                        _orderService.UseExistingBillingAddress(billingAddress,order);
                    }
                }

                if(model.ShippingAddress.UseExistingAddress )
                {
                    var shippingAddress = _userRepository.GetShippingAddress(user.Id);

                    if(!_orderService.MissingShippingAddress(shippingAddress))
                        _orderService.UseExistingShippingAddress(shippingAddress,order);
                }


        }

        public List<OrderModel> GetOrders(string userId)
        {
            var Orders = _orderRepository.GetOrdersByUserId(userId);
            return _mapper.Map<List<OrderModel>>(Orders);
        }

        public OrderModel GetOrderDetails(Guid Id)
        {
            var order = _orderRepository.GetById(Id);
            return _mapper.Map<OrderModel>(order);
        }


        public void SaveOrder(PlaceOrderModel model, Order order,string userId)
        {

                order.UserId = userId;
                order.Items = _mapper.Map<List<OrderItem>>(model.Items);
                order.Total = order.Items.Sum(x => x.ItemListing.Price * x.QuantityInCart); 
                _orderService.MapModelToOrder(model, order);
                _orderRepository.Add(order);
                _orderRepository.Commit();
        }

        public ShoppingCartModel GetShoppingCart(ApplicationUser user)
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
                var model = _mapper.Map<ShoppingCartModel>(cart);
                model.Total = cart.Items.Sum(x => x.ItemListing.Price* x.QuantityInCart);
                return model;
         }
    }
}