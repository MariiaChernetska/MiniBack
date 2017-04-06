using System;
using System.ComponentModel.DataAnnotations;

namespace ProcTest.Models.Database
{

    public class User
    {
        [Required]
        public Guid ID { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
    }
}