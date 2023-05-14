using System.ComponentModel.DataAnnotations;

namespace dotnet_mvc.Models
{
    public class SignupModel
    {
        [Required(ErrorMessage ="Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage ="Password confirmation is required")]
        [DataType (DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage ="First name is required")]
        public string FirstName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Phone number is required")]
        public string Phone { get; set; } = string.Empty;


        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; } = string.Empty;
    }
}
