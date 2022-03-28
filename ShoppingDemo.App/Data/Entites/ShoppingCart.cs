using System;
using System.Collections.Generic;

namespace ShoppingDemo.App.Data.Entites
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }

        public decimal Total { get; set; }
        public List<ShoppingCartItem> Items {get;set;}

        public ApplicationUser User {get;set;}
    }
}