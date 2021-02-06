using AutoMapper;
using Calvary.Data;
using Calvary.Extensions.Collection;
using Calvary.Providers;
using Calvary.ViewModels;
using Calvary.ViewModels.Activities.Income;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Calvary.Web.Areas.Activities.Controllers
{
    public class IncomeController : Controller
    {
        private readonly IncomeProvider incomeProvider;
        private readonly IncomeTypeProvider incomeTypeProvider;
        private readonly LookUpProvider lookUpProvider;

        public IncomeController(IncomeProvider incomeProvider, IncomeTypeProvider incomeTypeProvider, LookUpProvider lookUpProvider)
        {
            this.incomeProvider = incomeProvider;
            this.incomeTypeProvider = incomeTypeProvider;
            this.lookUpProvider = lookUpProvider;
        }

        // GET: Activities/Income
        public ActionResult Index()
        {
            var model = new IndexViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            if(form["btnDelete"] != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateEditViewModel();
            model.ReceivedDate = DateTime.Today;
            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection form, CreateEditViewModel model)
        {
            var income = new Income();
            Mapper.DynamicMap(model, income);
            incomeProvider.AddIncome(income);
            var jsonViewModel = new AjaxViewModel(true, income, null);
            return Json(jsonViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new CreateEditViewModel();
            var income = incomeProvider.GetIncome(id);
            Mapper.DynamicMap(income, model);            
            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection form, CreateEditViewModel model)
        {
            var income = incomeProvider.GetIncome(model.Id);
            Mapper.DynamicMap(model, income);            
            incomeProvider.UpdateIncome(income);
            var jsonViewModel = new AjaxViewModel(true, income, null);
            return Json(jsonViewModel);
        }


        public ActionResult List([DataSourceRequest] DataSourceRequest request, int month, int year)
        {
            var result = incomeProvider.GetIncomes(month, year);
            return Json(result.ToDataSourceResult(request));
        }   
        
        public ActionResult GetIncomeTypes()
        {
            var incomeTypes = incomeTypeProvider.GetIncomeTypes();
            return Json(incomeTypes, JsonRequestBehavior.AllowGet);
        }     

        public ActionResult GetMonths()
        {
            var months = Enumerable.Range(1, 12).Select(mon => new { Month = mon, MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(mon) });            
            return Json(months, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetYears()
        {
            var years = Enumerable.Range(DateTime.Today.Year - 2, 3);
            return Json(years, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCurrencies()
        {
            var currencies = lookUpProvider.GetLookUpValues("Currency");
            return Json(currencies, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> arrayOfId)
        {
            try
            {
                arrayOfId.ForEach(incomeProvider.DeleteIncome);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }
        }
    }
}