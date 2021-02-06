using System;
using System.Web.Mvc;
using AutoMapper;
using Calvary.Data;
using Calvary.Providers;
using Calvary.ViewModels;
using Calvary.ViewModels.Master.Ethnic;
using Calvary.Web.Base;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Calvary.Extensions.Collection;
using System.Collections.Generic;

namespace Calvary.Web.Areas.Master.Controllers
{
	public class EthnicController : BaseController
	{
        private readonly EthnicProvider ethnicProvider;
        private readonly KlasisProvider klasisProvider;
		public EthnicController(EthnicProvider ethnicProvider, KlasisProvider klasisProvider)            
        {
            this.klasisProvider = klasisProvider;
            this.ethnicProvider = ethnicProvider;
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
            var ethnic = ethnicProvider.GetEthnic(id);
            Mapper.DynamicMap(ethnic, model);

            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            var ethnic = ethnicProvider.GetEthnic(model.Id);
            Mapper.DynamicMap(model, ethnic);
            ethnicProvider.UpdateEthnic(ethnic);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            var ethnic = new Ethnic();                        
            Mapper.DynamicMap(model, ethnic);            
            ethnicProvider.AddEthnic(ethnic);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var list = ethnicProvider.List();
            return Json(list.ToDataSourceResult(request));
        }

        public ActionResult GetKlasisEthnic()
        {
            var klasisEthnics = klasisProvider.GetMappingValues("Ethnic");
            return Json(klasisEthnics, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> arrayOfId)
        {
            try
            {
                arrayOfId.ForEach(ethnicProvider.DeleteEthnic);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }            
        }		
    }
}
			