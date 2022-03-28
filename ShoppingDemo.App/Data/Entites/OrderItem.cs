using System;

namespace ShoppingDemo.App.Data.Entites
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        public Item ItemListing { get; set; }

        public int QuantityInCart { get; set; }

        public Order order { get; set; }
    }
}