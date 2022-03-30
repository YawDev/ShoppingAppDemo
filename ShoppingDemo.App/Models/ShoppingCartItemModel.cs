using System;

namespace Shopper.App.Models
{
    public class ShoppingCartItemModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid ItemId { get; set; }

        public decimal Price { get; set; }
        public string ImageFile { get; set; }

        public int QuantityInCart { get; set; }

        public ShoppingCartModel Cart { get; set; }
    }
}
