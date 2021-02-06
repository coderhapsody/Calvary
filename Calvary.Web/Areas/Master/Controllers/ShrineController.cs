using System;
using System.Web.Mvc;
using AutoMapper;
using Calvary.Data;
using Calvary.Providers;
using Calvary.ViewModels;
using Calvary.ViewModels.Master.Shrine;
using Calvary.Web.Base;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using Calvary.Extensions.Collection;

namespace Calvary.Web.Areas.Master.Controllers
{
    public class ShrineController : BaseController
    {
        private readonly ShrineProvider shrineProvider;
        public ShrineController(ShrineProvider shrineProvider)
        {
            this.shrineProvider = shrineProvider;
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
            var shrine = shrineProvider.GetShrine(id);
            Mapper.DynamicMap(shrine, model);

            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            var shrine = shrineProvider.GetShrine(model.Id);
            Mapper.DynamicMap(model, shrine);
            shrineProvider.UpdateShrine(shrine);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            var shrine = new Shrine();
            Mapper.DynamicMap(model, shrine);
            shrineProvider.AddShrine(shrine);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var list = shrineProvider.List();
            return Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> arrayOfId)
        {
            try
            {
                arrayOfId.ForEach(shrineProvider.DeleteShrine);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }
        }
    }
}






