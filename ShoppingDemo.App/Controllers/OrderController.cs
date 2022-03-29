using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopper.App.Models;
using ShoppingDemo.App.Data.Entites;
using ShoppingDemo.App.Mapping;
using ShoppingDemo.App.Services;
using ShoppingDemo.EFCore;

namespace ShoppingDemo.App.Controllers
{
    public class OrderController :  Controller
    {
       
        public OrderController(ILogger<OrderController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, 
        IOrderRepository orderRepository,ICryptoService cryptoService,IShoppingCartRepository shoppingCartRepository,IOrderService orderService,
        IUserRepository userRepository)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _orderRepository = orderRepository;
            _cryptoService = cryptoService;
            _shoppingCartRepository = shoppingCartRepository;
            _orderService = orderService;
            _userRepository = userRepository;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<EntityToQueryDtoMapper>()).CreateMapper();

        }

        public IActionResult ViewOrders()
        {
            if(_signInManager.IsSignedIn(User))
            {
                var user = _userManager.GetUserAsync(User).Result;
                var Orders = _orderRepository.GetOrdersByUserId(user.Id);
                var model = _mapper.Map<List<OrderModel>>(Orders);
                return View(model);
            }
            return RedirectToAction("Login");
        }


        public IActionResult ViewOrderDetail(Guid Id)
        {
            if(_signInManager.IsSignedIn(User))
            {
                var order = _orderRepository.GetById(Id);
                var model = _mapper.Map<OrderModel>(order);
                return View(model);
            }
            return RedirectToAction("Login");        }


        public IActionResult PlaceOrder()
        {
            if(_signInManager.IsSignedIn(User))
            {
                var user = _userManager.GetUserAsync(User).Result;
                var cart = _shoppingCartRepository.GetByUserId(user.Id);
                var order = _orderService.PrepareOrder(_mapper.Map<ShoppingCartModel>(cart));
                order.Total = order.Items.Sum(x => x.ItemListing.Price * x.QuantityInCart); 
                var orderModel = _mapper.Map<PlaceOrderModel>(order);

                return View(orderModel);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult PlaceOrder(PlaceOrderModel model)
        {
            if(ModelState.IsValid)
            {
                var order = _mapper.Map<Order>(model);
                if(model.Payment.UseExistingCard)
                {
                    var card = _userRepository.GetPaymentDetails(_userManager.GetUserId(User));


                    
                    var billingAddress = _userRepository.GetBillingAddress(_userManager.GetUserId(User));

                    if(!_orderService.MissingCardBilling(billingAddress, card))
                    {   
                        _orderService.UseExistingCard(card,order);
                        _orderService.UseExistingBillingAddress(billingAddress,order);
                    }
                }

                if(model.ShippingAddress.UseExistingAddress )
                {
                    var shippingAddress = _userRepository.GetShippingAddress(_userManager.GetUserId(User));

                    if(!_orderService.MissingShippingAddress(shippingAddress))
                        _orderService.UseExistingShippingAddress(shippingAddress,order);
                }

                var errors = _orderService.GetErrors();
                if(errors.Count > 0)
                {
                    var sb = new StringBuilder();
                    foreach(var error in errors)
                    {
                        sb.AppendLine(error.Value+",");
                        sb.AppendLine("\n");
                    }
                    ViewBag.Message = sb.ToString();
                    return View(model);
                }
                var user =_userManager.GetUserAsync(User).Result;
                order.Customer = new Customer
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                };

                order.UserId = user.Id;
                order.Items = _mapper.Map<List<OrderItem>>(model.Items);
                order.Total = order.Items.Sum(x => x.ItemListing.Price * x.QuantityInCart); 
                _orderService.MapModelToOrder(model, order);
                _orderRepository.Add(order);
                _orderRepository.Commit();
                
                return RedirectToAction("Index","Home");
            }
            return View(model);
        }


        private readonly ILogger<OrderController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICryptoService _cryptoService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
    }
}