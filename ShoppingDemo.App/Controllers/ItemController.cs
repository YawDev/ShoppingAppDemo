using System;
using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        IMapper _mapper {get;set;}
        IItemService _itemService;

        public ItemController(ILogger<ItemController> logger, SignInManager<ApplicationUser> signInManager, IMapper mapper, IItemService itemService)
        {
            _logger = logger;
            _signInManager = signInManager;
            _mapper = mapper;
            _itemService = itemService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddItemModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    _itemService.CreateItem(model);
                    return RedirectToAction("Index","Home");
                }
                return View(model);

            }
            catch(IOException e)
            {
                ViewBag.Message = e.Message;
                return View(model);
            }
        }


        public IActionResult Delete(Guid Id)
        {
            var item = _itemService.FindItem(Id);
            if(item is null)
                return RedirectToAction("Not Found");
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(Item item)
        {
            _itemService.DeleteItem(item);
            return RedirectToAction("Index","Home");
         
        }


        public IActionResult Edit(Guid Id)
        {
            var item = _itemService.FindItem(Id);
            if(item is null)
                return RedirectToAction("ResourceNotFound");
            
            return View(_mapper.Map<EditItemModel>(item));
        }

        [HttpPost]
        public IActionResult Edit(EditItemModel model)
        {
            _itemService.EditItem(model);
            return RedirectToAction("Index","Home");
         
        }


        public IActionResult ResourceNotFound()
        {
            return View();
        }

    }
}