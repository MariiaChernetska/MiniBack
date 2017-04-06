using ProcTest.Models.Database;
using ProcTest.Models.Enums;
using ProcTest.Models.ViewModels;
using ProcTest.Providers;
using ProcTest.Services;
using System;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ProcTest.Controllers
{
    [RoutePrefix("videos")]
    public class VideosController : BaseController
    {
        VideoService videoService = new VideoService();

        [Route("page/{pageNum}")]
        public IHttpActionResult GetAllVideos(int pageNum, string orderBy = "date", string order = "desc")
        {
            OrderDirection direction;
            OrderType type;
            switch (order) {
                case "desc": direction = OrderDirection.Descending; break;
                case "asc": direction = OrderDirection.Ascending; break;
                default: return BadRequest("Wrong sort order");
            }
            switch (orderBy)
            {
                case "date": type = OrderType.Date; break;
                case "rate": type = OrderType.Rating; break;
                default: return BadRequest("Wrong order type");
            }

            var res = videoService.GetVideosForMain(pageNum, type, direction, 8);
            

            if (res.Success)
            {
                return Ok(res.Data);
            }
            else
            {
                ModelState.AddModelError("", res.Error);
            }
            return BadRequest(ModelState);
        }

        [Route("block/{blockNum}")]
        public IHttpActionResult GetVideosBlock(int blockNum, string orderBy = "date", string order = "desc")
        {
            OrderDirection direction;
            OrderType type;
            switch (order)
            {
                case "desc": direction = OrderDirection.Descending; break;
                case "asc": direction = OrderDirection.Ascending; break;
                default: return BadRequest("Wrong sort order");
            }
            switch (orderBy)
            {
                case "date": type = OrderType.Date; break;
                case "rate": type = OrderType.Rating; break;
                default: return BadRequest("Wrong order type");
            }

            var res = videoService.GetVideosForMainBlock(blockNum, type, direction, 40);


            if (res.Success)
            {
                return Ok(res.Data);
            }
            else
            {
                ModelState.AddModelError("", res.Error);
            }
            return BadRequest(ModelState);
        }










        [Route("office/page/{pageNum}")]
        [Authorize]
        public IHttpActionResult GetOfficeVideos(int pageNum, string orderBy = "date", string order = "desc")
        {
            OrderDirection direction;
            OrderType type;
            switch (order)
            {
                case "desc": direction = OrderDirection.Descending; break;
                case "asc": direction = OrderDirection.Ascending; break;
                default: return BadRequest("Wrong sort order");
            }
            switch (orderBy)
            {
                case "date": type = OrderType.Date; break;
                case "rate": type = OrderType.Rating; break;
                default: return BadRequest("Wrong order type");
            }

            var res = videoService.GetVideosForUser(pageNum, type, direction, UserId.Value);
            if (res.Success)
            {
                return Ok(res.Data);
            }
            else
            {
                ModelState.AddModelError("", res.Error);
            }
            return BadRequest(ModelState);
        }

        [Route("player/{videoId}")]
        public IHttpActionResult GetVideo(Guid videoId)
        {
            var res = videoService.GetVideo(videoId);
            if (res.Success)
            {

                var buf = res.Data.Path;
                res.Data.Path = "/videos/videoFile/" + videoId;
                return Ok(res.Data);
            }
            else
            {
                ModelState.AddModelError("", res.Error);
            }
            return BadRequest(ModelState);
        }

        [Route("videosave")]
        [Authorize]
        public async Task<IHttpActionResult> PostFormData()
        {
            Video video = new Video();
            video.UserId = UserId.Value;
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/Uploads/Videos");
            var provider = new MyFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (var key in provider.FormData.AllKeys)
                {
                    if (provider.FileData.Count != 0)
                    {
                        foreach (MultipartFileData file in provider.FileData)
                        {
                            if (Path.GetExtension(file.LocalFileName) == ".mp4")
                            {
                                var capturer = new Capturer(Path.GetFileNameWithoutExtension(file.LocalFileName));
                                var paths = capturer.ff();
                                video.Path = paths.videoPath;
                                video.ScreenshotPath = paths.imagePath;
                                video.DateAdded = DateTime.UtcNow;
                            }
                            else return BadRequest("Wrong file extension");
                            
                        }
                    }
                    else
                    {
                        return BadRequest("Select file");
                    }
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        if (key == "title")
                        {
                            if (val == "")
                            {
                                return BadRequest("Title is required");
                            }
                            else
                            {
                                video.VideoName = val;
                                
                            }
                            
                        }
                        if (key == "description")
                        {
                            if (val == "")
                            {
                                return BadRequest("Description is required");
                            }
                            else
                            {
                                video.Description = val;
                            }
                            
                        }
                    }
                }
               
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var videoFromServer = videoService.SaveVideo(video);

                if (videoFromServer.Success)
                {
                    return Ok(videoFromServer.Data);
                }

                ModelState.AddModelError("", "All fields are required");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex);
            }

            return BadRequest(ModelState);
        }

        [Authorize]
        [Route("ratingsave")]
        public IHttpActionResult PostRating(RatingTakeViewModel r)
        {
            if (ModelState.IsValid)
            {
                var rating = new Rating
                {
                    UserId = UserId.Value,
                    Comment = r.Comment,
                    RatingNum = r.Rating,
                    DateAdded = DateTime.UtcNow,
                    VideoId = r.VideoId
                };
                var res = videoService.SaveRating(rating);
                if (res.Success)
                {
                    return Ok(res.Data);
                }
                ModelState.AddModelError("", res.Error);
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("videoFile/{videoId}")]
        public IHttpActionResult Generate(Guid videoId)
        {
            var res = videoService.GetVideo(videoId);
            if (!res.Success)
            {
                ModelState.AddModelError("", res.Error);
                return BadRequest(ModelState);
            }
            string root = HttpContext.Current.Server.MapPath("~/Uploads/Videos/");
            var path = root + res.Data.Path;
            return VideoGet(path);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                videoService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}