using System;

namespace Shopper.App.Models
{
    public class ShoppingCartItemModel
    {
        public Guid Id { get; set; }

        public ItemModel ItemListing { get; set; }

        public int QuantityInCart { get; set; }

        public ShoppingCartModel Cart { get; set; }
    }
}
