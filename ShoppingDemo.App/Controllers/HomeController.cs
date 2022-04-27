using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopper.App.Models;
using ShoppingDemo.App.Data.Entites;
using ShoppingDemo.App.Mapping;
using ShoppingDemo.EFCore;

namespace ShoppingDemo.App.Controllers
{
    public class HomeController: Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IItemRepository _itemRepository;

         IMapper _mapper {get;set;}

        public HomeController(ILogger<HomeController> logger, 
        IItemRepository itemRepository)
        {
            _logger = logger;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<EntityToQueryDtoMapper>()).CreateMapper();
            _itemRepository = itemRepository;

        }

        public IActionResult Index()
        {
            var items = _itemRepository.GetAll();
            var itemResults = _mapper.Map<IEnumerable<ItemModel>>(items);
            return View(itemResults);
        }

        public IActionResult Notfound()
        {
         
            return View();
        }

        public IActionResult UnderConstruction()
        {
         
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult ServerError()
        {
            return View();
        }

        



        
    }
}