using ProcTest.Models.ViewModels;
using ProcTest.Services;
using System.Web.Http;

namespace ProcTest.Controllers
{
    [RoutePrefix("")]
    public class AccountController : BaseController
    {
        UserService userService = new UserService();

        [AllowAnonymous]
        [Route("Register")]
        public IHttpActionResult PostUser(UserRegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                var res = userService.InsertUser(user);
                if (res.Success)
                {
                    return Ok(res.Data);
                }
                else
                {
                    ModelState.AddModelError("user.Login", res.Error);
                }
            }
            return BadRequest(ModelState);
        }

        [Route("token")]
        [HttpPost]
        public IHttpActionResult Login(UserLoginModel user)
        {
            if (ModelState.IsValid)
            {

                var res = userService.Authenticate(user.Login, user.Password);
                if (res.Success)
                {
                    return Ok(res.Data);
                }
                else
                {
                    ModelState.AddModelError("user.Login", res.Error);
                }
            }
            return BadRequest(ModelState);
        }
        [Route("primarylogin")]
        [Authorize]
        [HttpGet]
        public void PrimaryLogin() {
           
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                userService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
