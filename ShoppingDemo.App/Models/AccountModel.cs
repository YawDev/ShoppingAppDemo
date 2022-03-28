namespace Shopper.App.Models
{
    public class AccountModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public CardInfoModel CardInfo {get;set;}
        public AddressModel Address {get;set;}
    }
}
