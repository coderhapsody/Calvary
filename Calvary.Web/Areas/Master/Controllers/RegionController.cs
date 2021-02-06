using System;
using System.Web.Mvc;
using AutoMapper;
using Calvary.Data;
using Calvary.Providers;
using Calvary.ViewModels;
using Calvary.ViewModels.Master.Region;
using Calvary.Web.Base;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Calvary.Web.Areas.Master.Controllers
{
    public class RegionController : BaseController
    {
        private readonly RegionProvider regionProvider;

        public RegionController(RegionProvider regionProvider)
        {
            this.regionProvider = regionProvider;            
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
            var region = regionProvider.GetRegion(id);
            Mapper.DynamicMap(region, model);

            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            var region = regionProvider.GetRegion(model.Id);
            Mapper.DynamicMap(model, region);
            regionProvider.UpdateRegion(region);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            var region = new Region();
            Mapper.DynamicMap(model, region);
            regionProvider.AddRegion(region);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var list = regionProvider.List();
            return Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Delete(int[] arrayOfId)
        {
            try
            {
                Array.ForEach(arrayOfId, regionProvider.DeleteRegion);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }            
        }


        public ActionResult ValidateCode(string code, int id)
        {
            bool result = regionProvider.IsRegionCodeValid(code, id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}