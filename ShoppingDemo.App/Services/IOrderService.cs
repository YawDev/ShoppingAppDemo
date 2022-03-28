using System.Collections.Generic;
using AutoMapper;
using Shopper.App.Models;
using ShoppingDemo.App.Data.Entites;
using ShoppingDemo.App.Mapping;

namespace ShoppingDemo.App.Services
{
    public interface IOrderService
    {
        OrderModel PrepareOrder(ShoppingCartModel cart);
        Order MapModelToOrder(PlaceOrderModel orderModel, Order order);

        void UseExistingCard(PaymentCard card, Order order);
        void UseExistingShippingAddress(ShippingAddress address, Order order);
        void UseExistingBillingAddress(BillingAddress address, Order order);

        Order MapContactInfoToOrder(PlaceOrderModel orderModel, Order order);
    }

    public class OrderService : IOrderService
    {
        
        
        public OrderModel PrepareOrder(ShoppingCartModel cart)
        {
            var orderModel = new OrderModel()
            {
                Total = cart.Total,
                Items = new List<OrderItemModel>()
            };

            var cartItems = cart.Items;
            foreach(var cartItem in cartItems)
            {
                var orderItem = new OrderItemModel()
                {
                    QuantityInCart = cartItem.QuantityInCart,
                    Order = orderModel,
                    ItemListing = cartItem.ItemListing
                };
                orderModel.Items.Add(orderItem);
            }
            
            return orderModel;
        }

        public void UseExistingCard(PaymentCard card, Order order)
        {
            order.Card = new PaymentCard()
            {
                CardNumber = card.CardNumber,
                CVV = card.CVV,
                NameOnCard = card.NameOnCard,
            };
        }

        public void UseExistingShippingAddress(ShippingAddress address, Order order)
        {
            order.ShippingAddress = new ShippingAddress()
            {
                Addressline1 = address.Addressline1,
                Addressline2 = address.Addressline2,
                Addressline3 = address.Addressline3,
                State = address.State,
                Country = address.Country,
                Zipcode = address.Zipcode
            };
        }

        public void UseExistingBillingAddress(BillingAddress address, Order order)
        {
            order.Card.BillingAddress = new BillingAddress()
            {
                Addressline1 = address.Addressline1,
                Addressline2 = address.Addressline2,
                Addressline3 = address.Addressline3,
                State = address.State,
                Country = address.Country,
                Zipcode = address.Zipcode
            };
        }

        public Order MapContactInfoToOrder(PlaceOrderModel orderModel, Order order)
        {
            order.Email = orderModel.Email;
            order.OrderNumber = "111111";
            order.Phone = orderModel.Phone;
            return order;
        }

        public Order MapModelToOrder(PlaceOrderModel orderModel, Order order)
        {
            order.Email = orderModel.Email;
            order.OrderNumber = "111111";
            order.Phone = orderModel.Phone;
            order.Card = new PaymentCard();
            if(!orderModel.Payment.UseExistingCard)
            {
                order.Card.CardNumber = orderModel.Payment.CardNumber;
                order.Card.CVV = orderModel.Payment.CVV;
                order.Card.NameOnCard = orderModel.Payment.CardNumber;


                order.Card.BillingAddress = new BillingAddress();
                order.Card.BillingAddress.Addressline1 = orderModel.Payment.BillingAddress.Addressline1;
                order.Card.BillingAddress.Addressline2 = orderModel.Payment.BillingAddress.Addressline2;
                order.Card.BillingAddress.Addressline3 = orderModel.Payment.BillingAddress.Addressline3;
                order.Card.BillingAddress.Zipcode = orderModel.Payment.BillingAddress.Zipcode;
                order.Card.BillingAddress.Country = orderModel.Payment.BillingAddress.Country;
                order.Card.BillingAddress.State = orderModel.Payment.BillingAddress.State;
            }

        

            if(!orderModel.ShippingAddress.UseExistingAddress)
            {
                order.ShippingAddress = new ShippingAddress();
                order.ShippingAddress.Addressline1 = orderModel.ShippingAddress.Addressline1;
                order.ShippingAddress.Addressline2 = orderModel.ShippingAddress.Addressline2;
                order.ShippingAddress.Addressline3 = orderModel.ShippingAddress.Addressline3;
                order.ShippingAddress.Zipcode = orderModel.ShippingAddress.Zipcode;
                order.ShippingAddress.Country = orderModel.ShippingAddress.Country;
                order.ShippingAddress.State = orderModel.ShippingAddress.State;
            }

            return order;
        }
    }
}