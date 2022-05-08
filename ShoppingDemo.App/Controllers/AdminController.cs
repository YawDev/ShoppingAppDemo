using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ShoppingDemo.App.Data.Entites;
using ShoppingDemo.App.Mapping;
using ShoppingDemo.App.Models;
using ShoppingDemo.EFCore;

namespace ShoppingDemo.App.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        IMapper _mapper {get;set;}

        public AdminController(ILogger<AdminController> logger, 
        UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<EntityToQueryDtoMapper>()).CreateMapper();
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult ManageUsers()
        {   
            var allRoles = _roleManager.Roles.ToList();
            return View(allRoles);
        }

        public IActionResult AddToRole(string role)
        {
            var usersInRole = _userManager.GetUsersInRoleAsync(role).Result;
            var users = _userManager.Users.ToList().Where(s => !usersInRole.Any(x => x.Id == s.Id));
            List<SelectListItem> options =  new List<SelectListItem>();
            foreach(var user in users)
            {
                options.Add(new SelectListItem()
                {
                    Value = user.Id,
                    Text = user.FirstName+" "+user.LastName
                });
            }
            ViewBag.Role = role;
            ViewBag.Options = options;
            return View();
        }


        [HttpPost]
        public IActionResult AddToRole(AddUserToRoleViewModel model)
        {
            return View();
        }

        public IActionResult RemoveFromRole(string id)
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult RemoveFromRole()
        {
            return View();
        }
    }
}