using System;
using Microsoft.AspNetCore.Identity;

namespace ShoppingDemo.App.Data.Entites
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid CartSessionId{ get; set; }

    }


    
}