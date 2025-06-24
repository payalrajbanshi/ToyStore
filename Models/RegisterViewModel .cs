// using System.ComponentModel.DataAnnotations;

// namespace ToyStore.ViewModels
// {
//     public class RegisterViewModel
//     {
//         [Required, EmailAddress]
//         public string Email { get; set; }

//         [Required]
//         public string FullName { get; set; }

//         [Required]
//         [Phone]
//         public string PhoneNumber { get; set; }

//         [Required]
//         public string Address { get; set; }

//         // [Required, DataType(DataType.Password)]
//        [Compare("Password", ErrorMessage = "Passwords do not match.")]
//        [DataType(DataType.Password)]
//         public string Password { get; set; }

//         [Required, DataType(DataType.Password), Compare("Password")]
//         public string ConfirmPassword { get; set; }
//     }
// }
using System.ComponentModel.DataAnnotations;

namespace ToyStore.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}

