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
    public interface ICustomerOrderService
    {
         void ProcessOrder(PlaceOrderModel model,Order order, ApplicationUser user);

         string GetErrorMessage();

         void SaveOrder(PlaceOrderModel model, Order order,string userId);

         PlaceOrderModel MapOrderForm(string userid);

         List<OrderModel> GetOrders(string userId);

         OrderModel GetOrderDetails(Guid Id);

         Dictionary<string,string> GetModelErrors();

   
    }

    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICryptoService _cryptoService;
        private readonly IOrderService _orderService;
        private readonly IEmailService _emailService;

        IMapper _mapper;

        public CustomerOrderService(IOrderRepository orderRepository, IUserRepository userRepository, IShoppingCartRepository shoppingCartRepository, ICryptoService cryptoService, IOrderService orderService,
        IEmailService emailService)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _cryptoService = cryptoService;
            _orderService = orderService;
            _emailService = emailService;
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
            var cart = _shoppingCartRepository.GetById(user.CartSessionId);
            model.Total = cart.Total;
            order.Total = cart.Total;

        
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
                    else
                        _orderService.AddressFields(model.Payment.BillingAddress);
                }
                else
                    _orderService.PaymentFields(model);

                if(model.ShippingAddress.UseExistingAddress )
                {
                    var shippingAddress = _userRepository.GetShippingAddress(user.Id);

                    if(!_orderService.MissingShippingAddress(shippingAddress))
                        _orderService.UseExistingShippingAddress(shippingAddress,order);
                }
                else
                    _orderService.AddressFields(model.ShippingAddress);


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
                _orderService.MapModelToOrder(model, order);
                _orderRepository.Add(order);
                _orderRepository.Commit();
                SendOrderConfirmationEmail(order);
        }

        public void SendOrderConfirmationEmail(Order order)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Order Total: "+order.Total.ToString());
            var messageTemplate = new EmailTemplate(new List<string>(){order.Email}, "Your Order has been succesfully recieved.", sb.ToString());
            var message =_emailService.CreateEmailMessage(messageTemplate);
            _emailService.SendMessage(message);
        }

       
    }
}