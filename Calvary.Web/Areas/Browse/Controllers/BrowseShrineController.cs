using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Calvary.Providers;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Calvary.Web.Areas.Browse.Controllers
{
    public class BrowseShrineController : Controller
    {
        private readonly ShrineProvider shrineProvider;

        public BrowseShrineController(ShrineProvider shrineProvider)
        {
            this.shrineProvider = shrineProvider;
        }

        public ActionResult Index(string callback)
        {
            ViewBag.callback = callback;
            return View();
        }


        public ActionResult ListBrowseShrine([DataSourceRequest] DataSourceRequest request)
        {
            var list = shrineProvider.ListBrowse();
            return Json(list.ToDataSourceResult(request));
        }
    }
}