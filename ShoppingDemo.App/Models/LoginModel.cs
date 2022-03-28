using System.ComponentModel.DataAnnotations;

namespace Shopper.App.Models
{
    public class IdentityModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password {get;set;}
    }


    public class LoginModel : IdentityModel
    {
       
    }

    public class RegisterModel : IdentityModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword {get;set;}
    }
}
