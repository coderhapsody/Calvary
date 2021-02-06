
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Calvary.Providers;
using Calvary.ViewModels.Security.MenuAccess;
using Calvary.Data;
using AutoMapper;
using Calvary.ViewModels;

namespace Calvary.Web.Areas.Security.Controllers
{
    public class MenuAccessController : Controller
    {
        private readonly SecurityProvider securityProvider;

        public MenuAccessController(SecurityProvider securityProvider)
        {
            this.securityProvider = securityProvider;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListMenus(int? id)
        {
            var menus = securityProvider.ListAllMenus(id);            
            return Json(menus, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRolesFromMenu(int menuId)
        {
            var roles = securityProvider.GetRolesFromMenu(menuId);
            return Json(roles, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(FormCollection form, IList<RoleAccessViewModel> model)
        {
            if(model.Count > 0)
            {
                IList<RoleMenu> roleMenus = new List<RoleMenu>();
                Mapper.CreateMap<RoleAccessViewModel, RoleMenu>();
                roleMenus = Mapper.Map<IList<RoleAccessViewModel>, IList<RoleMenu>>(model.Where(m => m.RoleId != 0).ToList());
                securityProvider.UpdateRoleAccess(roleMenus);
            }
            var ajaxViewModel = new AjaxViewModel(true, model, null);
            return Json(ajaxViewModel);
        }
    }
}