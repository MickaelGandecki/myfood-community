using Microsoft.AspNet.Identity.Owin;
using myfoodapp.Hub.Business;
using Newtonsoft.Json;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace myfoodapp.Hub.Controllers
{
    public class RecipesController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [Authorize]
        public ActionResult Index()
        {
	        var data = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Content/GrowingModelv1.json"));
	        var model = JsonConvert.DeserializeObject<GrowingModel>(data);

			return View(model);
        }

    }
}
