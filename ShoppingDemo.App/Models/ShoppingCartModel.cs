using System.Collections.Generic;
using ShoppingDemo.App.Data.Entites;

namespace Shopper.App.Models
{
    public class ShoppingCartModel
    {
        public decimal Total { get; set; }

        public List<ShoppingCartItemModel> Items {get;set;}


    }
}
