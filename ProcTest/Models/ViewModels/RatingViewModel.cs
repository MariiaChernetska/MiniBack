using System;

namespace ProcTest.Models.ViewModels
{
    public class RatingViewModel
    {
        public string UserName { get; set; }

        public string Comment { get; set; }

        public int Rating { get; set; }

        public DateTime DateAdded { get; set; }
    }
   
}