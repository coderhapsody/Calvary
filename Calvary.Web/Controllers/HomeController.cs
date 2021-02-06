using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Calvary.Providers;
using Calvary.Web.Attributes;
using Calvary.Web.Base;
using Calvary.ViewModels.Dashboard;
using AutoMapper;
using System.Reflection;
using System.Diagnostics;
using Calvary.ViewModels;

namespace Calvary.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly SecurityProvider securityProvider;
        private readonly DashboardProvider dashboardProvider;
        public HomeController(SecurityProvider securityProvider, DashboardProvider dashboardProvider)
        {
            this.securityProvider = securityProvider;
            this.dashboardProvider = dashboardProvider;
        }

        public ActionResult Index()
        {
            var model = new DashboardViewModel();
            return View(model);
            
        }

        [AllowAnonymous]
        [HttpGet]
        [ImportModelStateFromTempData]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ExportModelStateToTempData]
        public ActionResult Login(FormCollection form)
        {
            string userName = form["username"];
            string password = form["password"];
            if(!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(password))
            {
                if(SecurityService.ValidateUser(userName, password))
                {
                    SecurityService.LogInUser(userName);
                    FormsAuthentication.SetAuthCookie(userName, false);
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("Login", "Invalid User Name or Password");
            return RedirectToAction("Login");
        }

        public ActionResult Navigation()
        {
            return PartialView("_Navigation");
        }


        public ActionResult About()
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            ViewBag.VersionNumber = currentAssembly.GetName().Version.ToString(3);
            ViewBag.Copyright = versionInfo.LegalCopyright;          
            return PartialView();
        }
        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult ChangePassword()
        {
            ViewBag.Message = TempData["message"];
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (securityProvider.ValidateUser(CurrentUserName, model.OldPassword))
                {
                    securityProvider.ChangePassword(CurrentUserName, model.NewPassword);
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    TempData["message"] = "Password lama salah.";
                    RedirectToAction("ChangePassword");
                }
            }
            return RedirectToAction("ChangePassword");
        }

        public ActionResult ChangePasswordSuccess() => View();

        public ActionResult GetRootMenus()
        {
            var rootMenus = securityProvider.ListMenus(null);
            return PartialView("_RootMenus", rootMenus);    
        }


        public ActionResult GetMenus(int parentMenuId)
        {
            var rootMenus = securityProvider.ListMenus(parentMenuId);
            return PartialView("_Menus", rootMenus);            
        }
    }
}