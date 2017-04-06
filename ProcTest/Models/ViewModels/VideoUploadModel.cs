using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcTest.Models.ViewModels
{
    public class VideoUploadModel
    {

        public int UserId { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string ScreenshotPath { get; set; }
        public string Description { get; set; }
    }
}