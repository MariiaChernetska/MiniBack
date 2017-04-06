using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcTest.Models.ViewModels
{
    public class VideoPlayerViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string ScreenshotPath { get; set; }
        public DateTime DateAdded { get; set; }
        public int Rating { get; set; }
        public int RatingsAmmount { get; set; }
        
        public bool IsCommented { get; set; }
    }
}