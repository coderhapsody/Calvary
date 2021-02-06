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
    public class BrowseMemberController : Controller
    {
        private readonly MemberProvider memberProvider;

        public BrowseMemberController(MemberProvider memberProvider)
        {
            this.memberProvider = memberProvider;
        }
        public ActionResult Index(string callback)
        {
            ViewBag.callback = callback;
            return View();
        }


        public ActionResult ListBrowseMember([DataSourceRequest] DataSourceRequest request)
        {
            var list = memberProvider.ListBrowse();
            return Json(list.ToDataSourceResult(request));
        }
    }
}