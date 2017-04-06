using System;
using System.ComponentModel.DataAnnotations;

namespace ProcTest.Models.Database
{
    public class Video
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
     
        public string Description { get; set; }
        [Required]
        public string VideoName { get; set; }

        public string Path { get; set; }

        public string ScreenshotPath { get; set; }

        public DateTime DateAdded { get; set; }  
    }
}