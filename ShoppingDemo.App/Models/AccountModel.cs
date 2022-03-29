namespace Shopper.App.Models
{
    public class AccountModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Username { get; set; }
        public string Email { get; set; }
        public CardInfoModel CardInfo {get;set;}
        public AddressModel Address {get;set;}
    }
}
