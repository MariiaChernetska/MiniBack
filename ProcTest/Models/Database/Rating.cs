using System;
using System.ComponentModel.DataAnnotations;


namespace ProcTest.Models.Database
{
    public class Rating
    {
        public Guid VideoId { get; set; }
      
        public Guid UserId { get; set; }
        
        public string Comment { get; set; }

        [Range(1, 5, ErrorMessage = "Raiting must be between 1 and 5")]
        public int RatingNum { get; set; }
        public DateTime DateAdded { get; set; }
    }
}