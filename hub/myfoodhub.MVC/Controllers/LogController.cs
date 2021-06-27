using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using myfoodapp.Hub.Models;
using myfoodapp.Hub.Services;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace myfoodapp.Hub.Controllers
{
    public class LogController : Controller
    {
        // GET: Log
        [Authorize]
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();

            return View();
        }

        [Authorize]
        public ActionResult Editing_Read([DataSourceRequest] DataSourceRequest request)
        {
            var db = new ApplicationDbContext();
            var messageService = new MessageService(db);

            var logs = db.Logs.Take(200).OrderByDescending(l => l.date).ToList();

            return Json(logs.ToDataSourceResult(request));
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}
