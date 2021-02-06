using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Calvary.Data;
using Calvary.Providers;
using Calvary.ViewModels;
using Calvary.ViewModels.Security.User;
using Calvary.Web.Base;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace Calvary.Web.Areas.Security.Controllers
{
    public class UserController : BaseController
    {
        private readonly SecurityProvider securityProvider;

        public UserController(SecurityProvider securityProvider)
        {
            this.securityProvider = securityProvider;
        }

        public ActionResult Index()
        {
            var model = new IndexViewModel();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new CreateEditViewModel();
            model.IsActive = true;
            return PartialView("Create", model);
        }

        public ActionResult Edit(int id)
        {
            var model = new CreateEditViewModel();
            var userLogin = securityProvider.GetUserLogin(id);
            Mapper.DynamicMap(userLogin, model);

            return PartialView("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            var userLogin = securityProvider.GetUserLogin(model.Id);
            Mapper.DynamicMap(model, userLogin);
            securityProvider.UpdateUserLogin(userLogin);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, CreateEditViewModel model)
        {
            var userLogin = new UserLogin();
            Mapper.DynamicMap(model, userLogin);
            userLogin.Password = form["password"];
            securityProvider.AddUserLogin(userLogin);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        public ActionResult GetRoles()
        {
            var roles = securityProvider.GetRoles().Select(role => new { role.Id, role.Name });
            return Json(roles, JsonRequestBehavior.AllowGet);
        }

        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var list = securityProvider.ListUserLogins();
            return Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Delete(int[] arrayOfId)
        {
            try
            {
                Array.ForEach(arrayOfId, securityProvider.DeleteUserLogin);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }
        }	
    }
}