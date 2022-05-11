using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Shopper.App.Models;
using ShoppingDemo.App.Data.Entites;
using ShoppingDemo.App.Mapping;
using ShoppingDemo.App.Services;
using ShoppingDemo.EFCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingDemo.App.Controllers
{
    public class IdentityController :  Controller
    {
        private readonly ILogger<IdentityController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository _userRepository;
        private readonly IUserDetailsComposition _userDetailsComposition;
        IConfiguration _configuration;


        IMapper _mapper;


        public IdentityController(ILogger<IdentityController> logger, SignInManager<ApplicationUser> signInManager,  UserManager<ApplicationUser> userManager,
        IUserRepository userRepository, IUserDetailsComposition userDetailsComposition, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _roleManager = roleManager;
            _userDetailsComposition = userDetailsComposition;
            _configuration = configuration;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<EntityToQueryDtoMapper>()).CreateMapper();

        }
       [AllowAnonymous]
        public IActionResult Login()
        {
            CreatePowerUser();
            return View();
        }
       [Authorize]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                
                var user = _userRepository.GetByUserName(model.Username);
                if(user == null)
                {
                    ViewBag.Message = "User not found.";
                    return View(model);
                }
                var isSuccess = _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                
                if(isSuccess.Result.Succeeded)
                    return RedirectToAction("Index", "Home");
                
                return View(model);
            }
        
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
           CreateDefaultRoles();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {

            if (ModelState.IsValid)
            {
                var user =  new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                }; 
                var userCreateResult = _userManager.CreateAsync(user,model.Password).Result;
                if(userCreateResult.Succeeded)
                {
                    _userRepository.CreateUser(user);
                    var result = _userManager.AddToRoleAsync(user, AppConstants.UserRole).Result;
                    _userRepository.Commit();
                    return RedirectToAction("Login");
                }
                var sb = new StringBuilder();
                foreach(var error in userCreateResult.Errors)
                {
                    sb.Append(error.Description+" ,");
                }
                ViewBag.Message = sb.ToString();

            }
            return View(model);
        }

       
       [Authorize]
        public IActionResult ViewAccount()
        {
           
                var user = _userRepository.GetByUserId(_userManager.GetUserId(User));
                var cardInfo = _userRepository.GetCardInformation(user.Id);
                var shippingAddress = _userRepository.GetShippingAddress(user.Id);
                var model = new AccountModel()
                {
                    Email = user.Email,
                    Username = user.UserName
                };
                if(cardInfo is null)
                    cardInfo = new CardInformation();

                if(shippingAddress is null)
                    shippingAddress = new ShippingAddress();

                model.Address = _mapper.Map<AddressModel>(shippingAddress);
                model.CardInfo = _mapper.Map<CardInfoModel>(cardInfo);
                return View(model);
    
        }

       [Authorize]
        public IActionResult EditAddress()
        {
       
            var shippingAddress = _userRepository.GetShippingAddress(_userManager.GetUserAsync(User).Result.Id);
            if(shippingAddress == null)
            {
                shippingAddress = new ShippingAddress();
            }

                
            return View(_mapper.Map<AddressModel>(shippingAddress));
        
        }

        [HttpPost]
        public IActionResult EditAddress(AddressModel model)
        {
            if(ModelState.IsValid)
            {
                var user = _userManager.GetUserAsync(User).Result;
                var shippingAddress = _userRepository.GetShippingAddress(user.Id);
                _userDetailsComposition.SaveShippingAddress(shippingAddress,model,user);
                return RedirectToAction("ViewAccount");
            }
            return View(model);
        }

       [Authorize]
        public IActionResult EditPaymentDetails()
        {
          
                var user = _userManager.GetUserAsync(User).Result;
                var cardInfo = _userRepository.GetCardInformation(user.Id);

                if(cardInfo is null)
                {
                    cardInfo = new CardInformation();
                    cardInfo.BillingAddress = new BillingAddress();
                }
                var model = new CardModel()
                {
                    NameOnCard = cardInfo.NameOnCard,
                    CVV = cardInfo.CVV,
                    CardNumber  = cardInfo.CardNumber,
                    BillingAddress = new AddressModel()
                    
                };
                model.BillingAddress.Addressline1 = cardInfo.BillingAddress.Addressline1;
                model.BillingAddress.Addressline2 = cardInfo.BillingAddress.Addressline2;
                model.BillingAddress.Addressline3 = cardInfo.BillingAddress.Addressline3;
                model.BillingAddress.State = cardInfo.BillingAddress.State;
                model.BillingAddress.Zipcode = cardInfo.BillingAddress.Zipcode;
                model.BillingAddress.Country = cardInfo.BillingAddress.Country;

                return View(model);
            
        }

        [HttpPost]
        public IActionResult EditPaymentDetails(CardModel model)
        {
            if(ModelState.IsValid)
            {
                var user = _userManager.GetUserAsync(User).Result;
                var paymentCard = _userRepository.GetPaymentDetails(user.Id);
                paymentCard = _userDetailsComposition.SavePaymentCard(paymentCard,model,user);

                var cardInfo = _userRepository.GetCardInformation(user.Id);
                _userDetailsComposition.SaveCardDetails(cardInfo, paymentCard,user);
                
                return RedirectToAction("ViewAccount");
            }
            return View(model);
        }


        public void CreateDefaultRoles()
        {
            if(!_roleManager.Roles.ToList().Any(x => x.Name == AppConstants.AdminRole))
                 _roleManager.CreateAsync(new IdentityRole(AppConstants.AdminRole));

            if(!_roleManager.Roles.ToList().Any(x => x.Name == AppConstants.UserRole))
                 _roleManager.CreateAsync(new IdentityRole(AppConstants.UserRole));
            
            var powerUser = _userRepository.GetByUserName(_configuration["PowerUser:UserName"]);
            if(powerUser != null)
            {
                var roleUsers = _userManager.GetUsersInRoleAsync("Admin");
                if(!roleUsers.Result.ToList().Any(x => x.UserName == powerUser.UserName))
                {
                    var result = _userManager.AddToRoleAsync(powerUser, "Admin").Result;
                }   
                _userRepository.Commit();
            }
            

        }

        public void CreatePowerUser()
        {
            var powerUser = _userRepository.GetByUserName(_configuration["PowerUser:UserName"]);
            if(powerUser == null)
            {
                powerUser =  new ApplicationUser
                {
                    Email = _configuration["PowerUser:Email"],
                    UserName = _configuration["PowerUser:UserName"],
                    FirstName = "Sa",
                    LastName = "Sudo"
                };

                 _userManager.CreateAsync(powerUser, _configuration["PowerUser:Password"]);
                _userRepository.CreateUser(powerUser);

                var result = _userManager.AddToRoleAsync(powerUser, AppConstants.AdminRole).Result;

            }
            
        }

        public IActionResult AccessDenied()
        {
            return View();
        }



    }
}