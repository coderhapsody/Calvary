using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Calvary.Web.Controllers
{
    [AllowAnonymous]
    public class ErrorsController : Controller
    {
        public ActionResult ResourceNotFound()
        {
            var logger = LogManager.GetLogger(this.GetType());
            logger.Error("Resource Not Found: " + Request["aspxerrorpath"]);
            return View();
        }

        public ActionResult Oops()
        {
            return View();
        }
    }
}