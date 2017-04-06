using System.ComponentModel.DataAnnotations;

namespace ProcTest.Models.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string RepeatPassword { get; set; }
    }
}