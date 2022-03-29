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

        private readonly ILogger<OrderController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        ICustomerComposition _customerComposition;
        IOrderService _orderService;
        IMapper _mapper;

        public OrderController(ILogger<OrderController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ICustomerComposition customerComposition, IOrderService orderService)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _customerComposition = customerComposition;
            _orderService = orderService;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<EntityToQueryDtoMapper>()).CreateMapper();
        }

        public IActionResult ViewOrders()
        {
            if(_signInManager.IsSignedIn(User))
            {
                var user = _userManager.GetUserAsync(User).Result;
                return View(_customerComposition.GetOrders(user.Id));
            }
            return RedirectToAction("Login");
        }


        public IActionResult ViewOrderDetail(Guid Id)
        {
            if(_signInManager.IsSignedIn(User))
                return View(_customerComposition.GetOrderDetails(Id));
            
            return RedirectToAction("Login");      
        }


        public IActionResult PlaceOrder()
        {
            if(_signInManager.IsSignedIn(User))
            {
                var user = _userManager.GetUserAsync(User).Result;
                return View(_customerComposition.MapOrderForm(user.Id));
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult PlaceOrder(PlaceOrderModel model)
        {
            var user =_userManager.GetUserAsync(User).Result;
            var order = _mapper.Map<Order>(model);

            if(ModelState.IsValid)
            {
                
                _customerComposition.ProcessOrder(model,order,user);
                var errors = _orderService.GetErrors();
                if(errors.Count > 0)
                {
                    ViewBag.Message = _customerComposition.GetModelErrors();
                    return View(model);
                }

                _customerComposition.SaveOrder(model,order,user.Id);
                return RedirectToAction("Index","Home");
            }
            return View(model);
        }


     
    
    }
}