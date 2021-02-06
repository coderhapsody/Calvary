using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Calvary.Providers;
using Calvary.ViewModels.Activities.Worship;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using AutoMapper;
using Calvary.Data;
using Calvary.Web.Attributes;
using Calvary.Web.Base;
using Calvary.ViewModels;

namespace Calvary.Web.Areas.Activities.Controllers
{
    public class WorshipController : BaseController
    {
        private WorshipProvider worshipProvider;
        private WorshipTypeProvider worshipTypeProvider;
        private IncomeProvider incomeProvider;
        private IncomeTypeProvider incomeTypeProvider;

        public WorshipController(
            WorshipProvider worshipProvider, 
            WorshipTypeProvider worshipTypeProvider, 
            IncomeProvider incomeProvider,
            IncomeTypeProvider incomeTypeProvider)
        {
            this.worshipProvider = worshipProvider;
            this.worshipTypeProvider = worshipTypeProvider;
            this.incomeProvider = incomeProvider;
            this.incomeTypeProvider = incomeTypeProvider;
        }

        #region Worship
        public ActionResult Index()
        {
            var model = new IndexViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            try
            {
                if (form["btnDelete"] != null)
                {
                    var worshipIds = form.GetValues("chkDelete");
                    if (worshipIds != null)
                    {
                        foreach (var worship in worshipIds)
                        {
                            var worshipId = Convert.ToInt32(worship);
                            worshipProvider.DeleteWorship(worshipId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
            }
            return RedirectToAction("Index");
        }
        
        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var list = worshipProvider.List();
            return Json(list.ToDataSourceResult(request));
        }

        [HttpGet]
        [ImportModelStateFromTempData]
        public ActionResult Create()
        {
            var model = new CreateEditViewModel();
            model.Date = DateTime.Today;
            return View("CreateEdit", model);
        }

        [HttpPost]
        [ExportModelStateToTempData]
        public ActionResult Create(FormCollection form, CreateEditViewModel model)
        {
            try
            {
                var worship = new Worship();
                Mapper.DynamicMap(model, worship);
                worshipProvider.AddWorship(worship);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return RedirectToAction("Create");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var worship = worshipProvider.GetWorship(id);
            var model = new CreateEditViewModel();
            Mapper.DynamicMap(worship, model);
            return View("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form, CreateEditViewModel model)
        {            
            try
            {
                var worship = worshipProvider.GetWorship(model.Id);
                Mapper.DynamicMap(model, worship);
                worshipProvider.UpdateWorship(worship);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                return RedirectToAction("Create");
            }
            return RedirectToAction("Index");
        }

        public ActionResult GetWorshipTypes()
        {
            var worshipTypes = worshipTypeProvider.GetWorshipTypes();
            return Json(worshipTypes, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region Income
        [HttpGet]
        public ActionResult IncomeIndex(int id)
        {
            var model = new IncomeIndexViewModel();
            model.SelectedWorship = worshipProvider.GetWorship(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult IncomeCreate(int worshipId)
        {
            var model = new IncomeCreateEditViewModel();
            var worship = worshipProvider.GetWorship(worshipId);
            model.Amount = 0;
            model.WorshipId = worshipId;
            model.ReceivedDate = worship.Date;
            return PartialView("IncomeCreateEdit", model);
        }

        [HttpPost]
        public ActionResult IncomeCreate(FormCollection form, IncomeCreateEditViewModel model)
        {
            var income = new Income();
            Mapper.DynamicMap(model, income);            
            incomeProvider.AddIncome(income);
            var jsonViewModel = new AjaxViewModel(true, income, null);
            return Json(jsonViewModel);
        }

        [HttpGet]
        public ActionResult IncomeEdit(int id)
        {
            var model = new IncomeCreateEditViewModel();
            var income =  incomeProvider.GetIncome(id);            
            Mapper.DynamicMap(income, model);
            return PartialView("IncomeCreateEdit", model);
        }

        [HttpPost]
        public ActionResult IncomeEdit(FormCollection form, IncomeCreateEditViewModel model)
        {
            var income = incomeProvider.GetIncome(model.Id);
            Mapper.DynamicMap(model, income);
            incomeProvider.UpdateIncome(income);
            var jsonViewModel = new AjaxViewModel(true, income, null);
            return Json(jsonViewModel);
        }

        public ActionResult GetIncomeTypes()
        {
            var incomeTypes = incomeTypeProvider.GetIncomeTypes();
            return Json(incomeTypes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListIncome([DataSourceRequest] DataSourceRequest request, int worshipId)
        {
            var list = incomeProvider.ListByWorship(worshipId);
            return Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult IncomeDelete(IEnumerable<int> arrayOfId)
        {
            try
            {
                Array.ForEach(arrayOfId.ToArray(), incomeProvider.DeleteIncome);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }
        }

        [HttpGet]
        public ActionResult GetLastNotes(int currentWorshipId)
        {
            string notes = worshipProvider.GetLastNotes(currentWorshipId);
            return Json(new AjaxViewModel(true, notes, null), JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}