using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Calvary.Data;
using Calvary.Providers;
using Calvary.ViewModels;
using Calvary.ViewModels.Master.EducationGrade;
using Calvary.Web.Base;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Collections.Generic;

namespace Calvary.Web.Areas.Master.Controllers
{
	public class EducationGradeController : BaseController
	{
        private readonly EducationGradeProvider educationGradeProvider;
        private readonly KlasisProvider klasisProvider;
        public EducationGradeController(KlasisProvider klasisProvider, EducationGradeProvider educationGradeProvider)            
        {
            this.klasisProvider = klasisProvider;
            this.educationGradeProvider = educationGradeProvider;
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
            var educationGrade = educationGradeProvider.GetEducationGrade(id);
            Mapper.DynamicMap(educationGrade, model);

            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            var educationGrade = new EducationGrade();
            Mapper.DynamicMap(model, educationGrade);
            educationGradeProvider.UpdateEducationGrade(educationGrade);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            var educationGrade = new EducationGrade();
            Mapper.DynamicMap(model, educationGrade);
            educationGradeProvider.AddEducationGrade(educationGrade);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            if (!request.Sorts.Any())
                request.Sorts.Add(new Kendo.Mvc.SortDescriptor(nameof(ListEducationGradeViewModel.Seq), System.ComponentModel.ListSortDirection.Ascending));
            var list = educationGradeProvider.List();
            return Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> arrayOfId)
        {
            try
            {
                Array.ForEach(arrayOfId.ToArray(), educationGradeProvider.DeleteEducationGrade);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }            
        }

        public ActionResult GetKlasisEducationGrade()
        {
            var klasisEducationGrades = klasisProvider.GetMappingValues("EducationGrade");
            return Json(klasisEducationGrades, JsonRequestBehavior.AllowGet);
        }
    }
}
			