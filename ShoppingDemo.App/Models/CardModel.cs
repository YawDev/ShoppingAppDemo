namespace Shopper.App.Models
{
    public class CardModel
    {
        public string NameOnCard { get; set; }

        public string CardNumber { get; set; }

        public string SecurityCode { get; set; }

        public string CVV { get; set; }

        public AddressModel BillingAddress { get; set; }

        public bool UseExistingCard { get; set; }
    }

    public class CardInfoModel :CardModel
    {
       
    }
}
