using System.Web.Mvc;

namespace ProcTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectPermanent("/swagger/ui/index");
        }
    }
}
