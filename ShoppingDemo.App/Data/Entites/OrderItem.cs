using System;

namespace ShoppingDemo.App.Data.Entites
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        public Guid ItemId { get; set; }

        public decimal Price { get; set; }
        public string ImageFile { get; set; }

        public int QuantityInCart { get; set; }

        public Order order { get; set; }
    }
}