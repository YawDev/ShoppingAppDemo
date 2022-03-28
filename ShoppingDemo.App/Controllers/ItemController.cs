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
using ShoppingDemo.EFCore;

namespace ShoppingDemo.App.Controllers
{
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IItemRepository _itemRepository;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IWebHostEnvironment _webHostEnv;
        IMapper _mapper {get;set;}



        public ItemController(ILogger<ItemController> logger, SignInManager<ApplicationUser> signInManager,  UserManager<ApplicationUser> userManager,
        IItemRepository itemRepository,IShoppingCartItemRepository shoppingCartItemRepository,IWebHostEnvironment webHostEnv)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _itemRepository = itemRepository;
            _webHostEnv = webHostEnv;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<EntityToQueryDtoMapper>()).CreateMapper();

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
                    var item =  new Item
                    {
                        Name = model.Name,
                        Price = model.Price,
                        Description = model.Description,
                        Quantity = model.Quantity,
                        inStock = model.inStock,
                    }; 

                    if(model.imageFile?.Length > 0)
                    {
                       item.Image = ToByteArray(model.imageFile);
                       item.FileName = Upload(model.imageFile);
                    } 
                    _itemRepository.Add(item);
                    _itemRepository.Commit();
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
            var item = _itemRepository.GetById(Id);
            if(item is null)
                return RedirectToAction("Not Found");
            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(Item item)
        {
            var cartItems = _shoppingCartItemRepository.GetAllByItemId(item.Id);
            _shoppingCartItemRepository.DeleteRange(cartItems);
           _itemRepository.Delete(item);
            _shoppingCartItemRepository.Commit();
            return RedirectToAction("Index","Home");
         
        }


        public IActionResult Edit(Guid Id)
        {
            var item = _itemRepository.GetById(Id);
            if(item is null)
                return RedirectToAction("ResourceNotFound");
            
            return View(_mapper.Map<EditItemModel>(item));
        }

        [HttpPost]
        public IActionResult Edit(EditItemModel model)
        {
            var item = _itemRepository.GetById(model.Id);
            item.Name = model.Name;
            item.Price = model.Price;
            item.Quantity = model.Quantity;
            item.inStock = model.inStock;
            item.Description = model.Description;

            if(model.imageFile?.Length > 0)
            {
                item.Image = ToByteArray(model.imageFile);
                item.FileName = Upload(model.imageFile);
            }
            
            _itemRepository.Commit();
            return RedirectToAction("Index","Home");
         
        }

        public string Upload(IFormFile file )
        {
            var filePath = Path.Combine(_webHostEnv.WebRootPath+"/images", file.FileName);
            SaveImage(file, filePath);
            return file.FileName;
        }


        public void SaveImage(IFormFile file, string path)
        {
            using(var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

        }

        public IActionResult ResourceNotFound()
        {
            return View();
        }


        public byte[] ToByteArray(IFormFile file)
        {
            using(var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}