using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class OrderController :  Controller
    {

        private readonly ILogger<OrderController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        ICustomerOrderService _customerComposition;
        IMapper _mapper;

        public OrderController(ILogger<OrderController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ICustomerOrderService customerComposition)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _customerComposition = customerComposition;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<EntityToQueryDtoMapper>()).CreateMapper();
        }

        public IActionResult ViewOrders()
        {
         
            var user = _userManager.GetUserAsync(User).Result;
            return View(_customerComposition.GetOrders(user.Id));
    
        }


        public IActionResult ViewOrderDetail(Guid Id)
        {
            return View(_customerComposition.GetOrderDetails(Id));
        }


        public IActionResult PlaceOrder()
        {
            var user = _userManager.GetUserAsync(User).Result;
            return View(_customerComposition.MapOrderForm(user.Id));
        }

        [HttpPost]
        public IActionResult PlaceOrder(PlaceOrderModel model)
        {
            var user =_userManager.GetUserAsync(User).Result;

            var order = new Order();

            if(ModelState.IsValid)
            {
                _customerComposition.ProcessOrder(model,order,user);
                var errors = _customerComposition.GetModelErrors();
                if(errors.Count > 0)
                {
                    ViewBag.Message = _customerComposition.GetErrorMessage();
                    return View(model);
                }

                _customerComposition.SaveOrder(model,order,user.Id);
                return RedirectToAction("Index","Home");
            }
            return View(_customerComposition.MapOrderForm(user.Id));
        }


     
    
    }
}