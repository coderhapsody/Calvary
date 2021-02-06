using System;
using System.Web.Mvc;
using System.Linq;
using AutoMapper;
using Calvary.Data;
using Calvary.Providers;
using Calvary.ViewModels;
using Calvary.ViewModels.Master.EducationMajor;
using Calvary.Web.Base;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Collections.Generic;

namespace Calvary.Web.Areas.Master.Controllers
{
	public class EducationMajorController : BaseController
	{
        private readonly EducationMajorProvider educationMajorProvider;
		public EducationMajorController(EducationMajorProvider educationMajorProvider)            
        {			
            this.educationMajorProvider = educationMajorProvider;
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
            var educationMajor = educationMajorProvider.GetEducationMajor(id);
            Mapper.DynamicMap(educationMajor, model);

            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            var educationMajor = educationMajorProvider.GetEducationMajor(model.Id);
            Mapper.DynamicMap(model, educationMajor);
            educationMajorProvider.UpdateEducationMajor(educationMajor);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            var educationMajor = new EducationMajor();
            Mapper.DynamicMap(model, educationMajor);
            educationMajorProvider.AddEducationMajor(educationMajor);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var list = educationMajorProvider.List();
            return Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> arrayOfID)
        {
            try
            {
                Array.ForEach(arrayOfID.ToArray(), educationMajorProvider.DeleteEducationMajor);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }            
        }		
    }
}
			