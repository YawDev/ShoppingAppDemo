using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingDemo.App.Data.Enums;

namespace ShoppingDemo.App.Data.Entites
{
    public class Order
    {
        public Guid Id { get; set; }

        public DateTime DateOrdered { get; set; }

        public string OrderNumber { get; set; }

        public OrderStatus Status { get; set; }

        public decimal Total { get; set; }

        public List<OrderItem> Items {get;set;}

        public string UserId { get; set; }

        public Customer Customer {get;set;}

        public string Email { get; set; }

        public string Phone { get; set; }

        public ShippingAddress ShippingAddress { get; set; }

        public PaymentCard Card { get; set; }

        public Order()
        {
            Items = new List<OrderItem>();
            Total = Items.Sum(x => x.Price * x.QuantityInCart);
        }

    }
}