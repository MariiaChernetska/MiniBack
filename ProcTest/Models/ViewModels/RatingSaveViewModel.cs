using System;

namespace ProcTest.Models.ViewModels
{
    public class RatingSaveViewModel
    {
        public string Comment { get; set; }

        public int Rating { get; set; }

        public DateTime DateAdded { get; set; }

        public Guid UserId { get; set; }

        public Guid VideoId { get; set; }
    }
}