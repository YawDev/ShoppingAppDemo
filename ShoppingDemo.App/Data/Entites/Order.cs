using System;
using System.Collections.Generic;

namespace ShoppingDemo.App.Data.Entites
{
    public class Order
    {
        public Guid Id { get; set; }

        public string OrderNumber { get; set; }

        public decimal Total { get; set; }

        public List<OrderItem> Items {get;set;}

        public ApplicationUser User {get;set;}

        public string Email { get; set; }

        public string Phone { get; set; }

        public ShippingAddress ShippingAddress { get; set; }

        public PaymentCard Card { get; set; }

    }
}