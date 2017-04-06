using ProcTest.Providers;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ProcTest.Controllers
{
    public class BaseController : ApiController
    {
        public Guid? UserId
        {
            get
            {
                var claim = ClaimsPrincipal.Current.Claims.FirstOrDefault(c => c.Type == "userId");
                if (claim == null)
                {
                    return null;
                }
                return Guid.Parse(claim.Value);
            }
        }

        //public IHttpActionResult VideoDownload(string filePath)
        //{
        //    var result = new HttpResponseMessage(HttpStatusCode.OK)
        //    {

        //        Content = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath))
        //    };
        //    var fileName = System.IO.Path.GetFileName(filePath);
        //    result.Content.Headers.ContentDisposition =
        //        new ContentDispositionHeaderValue("attachment")
        //        {
        //            FileName = fileName
        //        };
        //    result.Content.Headers.ContentType =
        //        new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(fileName));

        //    return ResponseMessage(result);
        //}
        public IHttpActionResult VideoGet(string filename)
        {
            var video = new VideoStreamProvider(filename);

            var response = Request.CreateResponse();
            response.Content = new PushStreamContent(async (Stream outputStream, HttpContent content, TransportContext context) =>
            {
                try
                {
                    var buffer = new byte[4096];

                    using (var videoVar = File.Open(filename, FileMode.Open, FileAccess.Read))
                    {
                        var length = (int)videoVar.Length;
                        var bytesRead = 1;

                        while (length > 0 && bytesRead > 0)
                        {
                            bytesRead = videoVar.Read(buffer, 0, Math.Min(length, buffer.Length));
                            await outputStream.WriteAsync(buffer, 0, bytesRead);
                            length -= bytesRead;
                        }
                    }
                }
                finally
                {
                    outputStream.Close();
                }
            });

            return ResponseMessage(response);
        }
    }
}