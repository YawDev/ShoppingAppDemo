using System.Collections.Generic;
using ShoppingDemo.App.Data.Entites;
using ShoppingDemo.App.Models;

namespace Shopper.App.Models
{
    public class ShoppingCartModel
    {
        public decimal Total { get; set; }

        public List<ShoppingCartItemModel> Items {get;set;}

    }

    public class ManageRolesViewModel
    {
        public List<UserModel> Users { get; set; }
    }
}
