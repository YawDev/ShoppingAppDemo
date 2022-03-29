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
        void NoContactProvided(PlaceOrderModel orderModel);

        void UseExistingContactInfo(ApplicationUser user, Order order, OrderModel model);

        void UseExistingCard(PaymentCard card, Order order);
        void UseExistingShippingAddress(ShippingAddress address, Order order);
        void UseExistingBillingAddress(BillingAddress address, Order order);

        bool MissingCardBilling(BillingAddress address, PaymentCard card);
        bool MissingShippingAddress(ShippingAddress address);

        Dictionary<string,string> GetErrors();

        Order MapContactInfoToOrder(PlaceOrderModel orderModel, Order order);
    }

    public class OrderService : IOrderService
    {
        Dictionary<string,string> Errors;

        public OrderService()
        {
            Errors = new Dictionary<string, string>();
        }
        
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

        public bool MissingCardBilling(BillingAddress address, PaymentCard card)
        {
            if(address == null || card == null)
            {
                Errors.Add("CardBillingAddress", "No Existing Card / Billing Address");
                return true;
            }
            return false;
                
        }


        public bool MissingShippingAddress(ShippingAddress address)
        {
            if(address == null)
            {
                Errors.Add("ShippingAddress", "No Existing Address for Shipping");
                return true;
            }
            return false;
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
            order.Total = orderModel.Total;
            if(!orderModel.Payment.UseExistingCard)
            {
                order.Card.CardNumber = orderModel.Payment.CardNumber;
                order.Card.CVV = orderModel.Payment.CVV;
                order.Card.NameOnCard = orderModel.Payment.CardNumber;
                order.Card.Expiry = orderModel.Payment.Expiry;



                order.Card.BillingAddress = new BillingAddress();
                order.Card.BillingAddress.Addressline1 = orderModel.Payment.BillingAddress.Addressline1;
                order.Card.BillingAddress.Addressline2 = orderModel.Payment.BillingAddress.Addressline2;
                order.Card.BillingAddress.Addressline3 = orderModel.Payment.BillingAddress.Addressline3;
                order.Card.BillingAddress.Zipcode = orderModel.Payment.BillingAddress.Zipcode;
                order.Card.BillingAddress.Country = orderModel.Payment.BillingAddress.Country;
                order.Card.BillingAddress.State = orderModel.Payment.BillingAddress.State;
                order.Card.BillingAddress.City = orderModel.Payment.BillingAddress.City;

            }

        

            if(!orderModel.ShippingAddress.UseExistingAddress)
            {
                order.ShippingAddress = new ShippingAddress();
                order.ShippingAddress.Addressline1 = orderModel.ShippingAddress.Addressline1;
                order.ShippingAddress.Addressline2 = orderModel.ShippingAddress.Addressline2;
                order.ShippingAddress.Addressline3 = orderModel.ShippingAddress.Addressline3;
                order.ShippingAddress.Zipcode = orderModel.ShippingAddress.Zipcode;
                order.ShippingAddress.Country = orderModel.ShippingAddress.Country;
                order.ShippingAddress.City = orderModel.ShippingAddress.City;
                order.ShippingAddress.State = orderModel.ShippingAddress.State;
            }

            if(!orderModel.UseExistingContactInfo)
            {
                order.Customer = new Customer
                {
                    FirstName = orderModel.FirstName,
                    LastName = orderModel.LastName,
                    Email = orderModel.Email,
                };
            }

            return order;
        }

        public Dictionary<string, string> GetErrors()
        {
            return Errors;
        }

        public void UseExistingContactInfo(ApplicationUser user, Order order, OrderModel model)
        {
            order.Customer = new Customer
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            model.FirstName = order.Customer.FirstName;
            model.LastName = order.Customer.LastName;
            model.Email = order.Customer.Email;
        }

        public void NoContactProvided(PlaceOrderModel orderModel)
        {
            if(string.IsNullOrEmpty(orderModel.FirstName))
                Errors.Add("FirstName", "First Name Missing");

            if(string.IsNullOrEmpty(orderModel.LastName))
                Errors.Add("LastName", "Last Name Missing");

            if(string.IsNullOrEmpty(orderModel.Email))
                Errors.Add("Email", "Email Missing");
        }
    }
}