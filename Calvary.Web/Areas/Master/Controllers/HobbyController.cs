using System;
using System.Web.Mvc;
using AutoMapper;
using Calvary.Data;
using Calvary.Providers;
using Calvary.ViewModels;
using Calvary.ViewModels.Master.Hobby;
using Calvary.Web.Base;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using Calvary.Extensions.Collection;

namespace Calvary.Web.Areas.Master.Controllers
{
    public class HobbyController : BaseController
    {
        private readonly HobbyProvider hobbyProvider;
        public HobbyController(HobbyProvider hobbyProvider)
        {
            this.hobbyProvider = hobbyProvider;
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
            var hobby = hobbyProvider.GetHobby(id);
            Mapper.DynamicMap(hobby, model);

            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            var hobby = hobbyProvider.GetHobby(model.Id);
            Mapper.DynamicMap(model, hobby);
            hobbyProvider.UpdateHobby(hobby);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            var hobby = new Hobby();
            Mapper.DynamicMap(model, hobby);
            hobbyProvider.AddHobby(hobby);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var list = hobbyProvider.List();
            return Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> arrayOfId)
        {
            try
            {
                arrayOfId.ForEach(hobbyProvider.DeleteHobby);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }
        }
    }
}
