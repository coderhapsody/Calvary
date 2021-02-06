using System;
using System.Web.Mvc;
using AutoMapper;
using Calvary.Data;
using Calvary.Providers;
using Calvary.ViewModels;
using Calvary.ViewModels.Master.Skill;
using Calvary.Web.Base;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using Calvary.Extensions.Collection;

namespace Calvary.Web.Areas.Master.Controllers
{
	public class SkillController : BaseController
	{
        private readonly SkillProvider skillProvider;
		public SkillController(SkillProvider skillProvider)            
        {			
            this.skillProvider = skillProvider;
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
            var skill = skillProvider.GetSkill(id);
            Mapper.DynamicMap(skill, model);

            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            var skill = skillProvider.GetSkill(model.Id);
            Mapper.DynamicMap(model, skill);
            skillProvider.UpdateSkill(skill);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            var skill = new Skill();
            Mapper.DynamicMap(model, skill);
            skillProvider.AddSkill(skill);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var list = skillProvider.List();
            return Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> arrayOfId)
        {
            try
            {
                arrayOfId.ForEach(skillProvider.DeleteSkill);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }            
        }		
    }
}
			