using System;

namespace ShoppingDemo.App.Data.Entites
{
    public class ShoppingCartItem
    {
        public Guid Id { get; set; }

        public Item ItemListing { get; set; }

        public int QuantityInCart { get; set; }

        public ShoppingCart Cart { get; set; }
    }
}