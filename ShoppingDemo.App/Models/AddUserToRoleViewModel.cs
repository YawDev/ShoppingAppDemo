using Microsoft.AspNetCore.Mvc;

namespace ShoppingDemo.App.Models
{
    public class AddUserToRoleViewModel
    {
    [   BindProperty(Name = "Users")]
        public string user {get; set;}

        [BindProperty(Name = "RoleInput")]
        public string Role { get; set; }
    }

  
}