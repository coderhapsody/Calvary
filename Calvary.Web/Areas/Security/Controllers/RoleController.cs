using System;
using System.Web.Mvc;
using AutoMapper;
using Calvary.Data;
using Calvary.Providers;
using Calvary.ViewModels;
using Calvary.ViewModels.Security.Role;
using Calvary.Web.Base;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Calvary.Web.Areas.Security.Controllers
{
	public class RoleController : BaseController
	{
        private readonly SecurityProvider securityProvider;
        public RoleController(SecurityProvider securityProvider)            
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
            return PartialView("CreateEdit", model);
        }

        public ActionResult Edit(int id)
        {
            var model = new CreateEditViewModel();
            var role = securityProvider.GetRole(id);
            Mapper.DynamicMap(role, model);

            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            var role = securityProvider.GetRole(model.Id);
            Mapper.DynamicMap(model, role);
            securityProvider.UpdateRole(role);

            var jsonViewModel = new AjaxViewModel(true, role, null);
            return Json(jsonViewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            var role = new Role();
            Mapper.DynamicMap(model, role);
            securityProvider.AddRole(role);

            var jsonViewModel = new AjaxViewModel(true, role, null);
            return Json(jsonViewModel);
        }

        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var list = securityProvider.List();
            return Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Delete(int[] arrayOfId)
        {
            try
            {
                Array.ForEach(arrayOfId, securityProvider.DeleteRole);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }            
        }		
    }
}
			