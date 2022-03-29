using System;
using ShoppingDemo.App.Data.Enums;

namespace ShoppingDemo.App.Data.Entites
{
    public class CardInformation 
    {
        public Guid Id {get;set;}

        public string NameOnCard { get; set; }

        public string CardNumber { get; set; }

        public DateTime Expiry { get; set; }

        public CardType CardType { get; set; }

        public string CVV { get; set; }

        public BillingAddress BillingAddress { get; set; }
        public ApplicationUser User {get;set;}

    }
    public class PaymentCard : CardInformation
    {
        


    }
}