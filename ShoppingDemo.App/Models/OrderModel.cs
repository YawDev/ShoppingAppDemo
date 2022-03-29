using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shopper.App.Models
{
    public class OrderModel 
    {
        public Guid Id { get; set; }

        public string OrderNumber { get; set; }

        public decimal Total { get; set; }

        public string Status { get; set; }

        public List<OrderItemModel> Items {get;set;}

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public AddressModel ShippingAddress { get; set; }

        public CardModel Payment {get;set;}

    }

    public class PlaceOrderModel : OrderModel
    {
    }

    public class OrderItemModel
    {
        public Guid Id { get; set; }

        public ItemModel ItemListing { get; set; }

        public int QuantityInCart { get; set; }

        public OrderModel Order { get; set; }
    }
}
