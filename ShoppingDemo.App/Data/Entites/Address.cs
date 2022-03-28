using System;

namespace ShoppingDemo.App.Data.Entites
{
    public class Address
    {
        public Guid Id {get;set;}
        public string Addressline1 { get; set; }
        public string Addressline2 { get; set; }
        public string Addressline3 { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public ApplicationUser User { get; set; }

    }

    public class ShippingAddress : Address
    {

    }

    public class BillingAddress : Address
    {

    }

    
}