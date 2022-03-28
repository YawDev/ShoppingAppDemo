using System;

namespace ShoppingDemo.App.Data.Entites
{
    public class Item
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }
        public string FileName { get; set; }
        public bool inStock { get; set; }

        public int Quantity { get; set; }


    }
}