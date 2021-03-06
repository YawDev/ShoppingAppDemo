using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ILogger<ShoppingCartController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        IShoppingCartService _shoppingCartService;
        IItemService _itemService;
        IMapper _mapper;

        public ShoppingCartController(ILogger<ShoppingCartController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IShoppingCartService shoppingCartService, IItemService itemService)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _shoppingCartService = shoppingCartService;
            _itemService = itemService;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<EntityToQueryDtoMapper>()).CreateMapper();        
        }

        public IActionResult ViewCart()
        {
       
            var user = _userManager.GetUserAsync(User).Result;
            return View(_shoppingCartService.MapShoppingCartModel(user));

        }

        public IActionResult AddToCart(Guid Id)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var cart = _shoppingCartService.GetShoppingCart(user);
            var item = _itemService.FindItem(Id);
            return View(_mapper.Map<ItemModel>(item));
       
        }

        [HttpPost]
        public IActionResult AddToCart(ItemModel model)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var cart = _shoppingCartService.GetShoppingCart(user);

           _shoppingCartService.AddCartItem(cart, model);
            return RedirectToAction("ViewCart");
        }




        public IActionResult RemoveFromCart(Guid Id)
        {

            var user = _userManager.GetUserAsync(User).Result;
            var cart = _shoppingCartService.GetShoppingCart(user);
            var item = _itemService.FindItem(Id);
            return View(_mapper.Map<ItemModel>(item));
     
        }
        [HttpPost]
        public IActionResult RemoveFromCart(ItemModel model)
        {
            var user = _userManager.GetUserAsync(User).Result;
            var cart = _shoppingCartService.GetShoppingCart(user);
            _shoppingCartService.RemoveCartItem(cart, model);
            return RedirectToAction("ViewCart");
        }
    }
}