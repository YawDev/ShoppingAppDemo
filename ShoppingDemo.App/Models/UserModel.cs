using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingDemo.App.Models
{
    public class UserModel
    {
        public string Id {get;set;}
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }

    public class AddUserToRoleViewModel
    {
    [   BindProperty(Name = "Users")]
        public string user {get; set;}
    }

  
}