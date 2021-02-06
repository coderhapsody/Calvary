using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Calvary.Data;
using Calvary.Providers;
using Calvary.ViewModels;
using Calvary.ViewModels.Master.IncomeType;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Calvary.Extensions.Collection;

namespace Calvary.Web.Areas.Master.Controllers
{
    public class IncomeTypeController : Controller
    {
        private IncomeTypeProvider incomeTypeProvider;

        public IncomeTypeController(IncomeTypeProvider incomeTypeProvider)
        {
            this.incomeTypeProvider = incomeTypeProvider;
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
            var incomeType = incomeTypeProvider.GetIncomeType(id);
            Mapper.DynamicMap(incomeType, model);

            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            var incomeType = incomeTypeProvider.GetIncomeType(model.Id);
            Mapper.DynamicMap(model, incomeType);
            incomeTypeProvider.UpdateIncomeType(incomeType);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            var incomeType = new IncomeType();
            Mapper.DynamicMap(model, incomeType);
            incomeTypeProvider.AddIncomeType(incomeType);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var list = incomeTypeProvider.List();
            return Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> arrayOfId)
        {
            try
            {
                arrayOfId.ForEach(incomeTypeProvider.DeleteIncomeType);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }
        }

    }
}