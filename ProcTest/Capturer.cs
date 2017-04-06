using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace ProcTest
{
    public class VideoPaths {
       public string videoPath = "", imagePath = "";

    }
    public class Capturer {
        string fileName = "ffmpeg.exe";
        string video;
        string image;

        public Capturer(string fileName) {
            this.video = fileName+".mp4";
            this.image = fileName+".jpg";
        }
        
      
        
        
        string ffmpegPath;
        string videoFilename;
        string imageFilename;

        string workingDirectory;
        System.TimeSpan captureTime = new TimeSpan(0, 0, 1);
        int timeout = 300;

        void CreatePaths() {
            ffmpegPath = HttpContext.Current.Server.MapPath("~/"+fileName); ;
            videoFilename = HttpContext.Current.Server.MapPath("~/Uploads/Videos/"+video);
            imageFilename = HttpContext.Current.Server.MapPath("~/Uploads/Previews/" + image);
            workingDirectory = HttpContext.Current.Server.MapPath("~/Uploads/Videos/");

        }
        public VideoPaths ff()
        {
            CreatePaths();
            Process ffmpeg = new Process();
            ffmpeg.EnableRaisingEvents = true;
            ffmpeg.StartInfo = new ProcessStartInfo
            {
                FileName = this.ffmpegPath,
                Arguments = string.Format(
                    "-i \"{0}\" -an -y -s 320x240 -ss {1} -vframes 1 -f image2 \"{2}\"",
                    this.videoFilename,
                    DateTime.MinValue.Add(this.captureTime).ToString("HH:mm:ss", CultureInfo.InvariantCulture),
                    this.imageFilename
                ),
                WorkingDirectory = this.workingDirectory,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            ffmpeg.Start();
            ffmpeg.WaitForExit(this.timeout);
            return new VideoPaths { videoPath = video, imagePath = "/Uploads/Previews/"+image };
            }
    }
}