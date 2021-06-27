using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myfoodapp.Hub.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Error500()
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}