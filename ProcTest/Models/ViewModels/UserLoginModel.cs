using System.ComponentModel.DataAnnotations;

namespace ProcTest.Models.ViewModels
{
    public class UserLoginModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}