using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Calvary.Data;
using Calvary.Providers;
using Calvary.ViewModels;
using Calvary.ViewModels.Master.WorshipType;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Calvary.Extensions.Collection;

namespace Calvary.Web.Areas.Master.Controllers
{
    public class WorshipTypeController : Controller
    {
        private readonly WorshipTypeProvider worshipTypeProvider;
        public WorshipTypeController(WorshipTypeProvider worshipTypeProvider)
        {
            this.worshipTypeProvider = worshipTypeProvider;
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
            return PartialView("CreateEdit", model);
        }

        public ActionResult Edit(int id)
        {
            var model = new CreateEditViewModel();
            var worshipType = worshipTypeProvider.GetWorshipType(id);
            Mapper.DynamicMap(worshipType, model);

            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            var hobby = worshipTypeProvider.GetWorshipType(model.Id);
            Mapper.DynamicMap(model, hobby);
            worshipTypeProvider.UpdateWorshipType(hobby);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            var worshipType = new WorshipType();
            Mapper.DynamicMap(model, worshipType);
            worshipTypeProvider.AddWorshipType(worshipType);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var list = worshipTypeProvider.List();
            return Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> arrayOfId)
        {
            try
            {
                arrayOfId.ForEach(worshipTypeProvider.DeleteWorshipType);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }
        }
    }
}