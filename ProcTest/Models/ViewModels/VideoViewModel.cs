using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcTest.Models.ViewModels
{
    public class VideoViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string ScreenShot { get; set; }
        public DateTime DateAdded {get; set; }
        public int Rating { get; set; }
        public int RatingsAmount { get; set; }
        public string Description { get; set; }

    }
    public class CountViewModel: VideoViewModel
    {
        public int Count { get; set; }
    }
    public class VideoRatingsViewModel : VideoViewModel
    {
        
        public List<RatingViewModel> Ratings { get; set; }
        
    }
    public class MainPageVideoViewModel
    {
        public List<VideoViewModel> VideosArray { get; set; }

        public int PagesAmount { get; set; }
        public int PageNum { get; set; }
    }
}