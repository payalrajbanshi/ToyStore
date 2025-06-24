using System.ComponentModel.DataAnnotations;

namespace ToyStore.ViewModels
{
    // public class RegisterViewModel
    // {
    //     [Required, EmailAddress]
    //     public string Email { get; set; }

    //     [Required]
    //     public string FullName { get; set; }

    //     [Required, DataType(DataType.Password)]
    //     public string Password { get; set; }

    //     [Required, DataType(DataType.Password), Compare("Password")]
    //     public string ConfirmPassword { get; set; }
    // }

    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
