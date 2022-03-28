using System.ComponentModel.DataAnnotations;

namespace Shopper.App.Models
{
    public class AddressModel
    {
        public string Addressline1 { get; set; }
        public string Addressline2 { get; set; }
        public string Addressline3 { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public bool UseExistingAddress { get; set; }
    }
}
