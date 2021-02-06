using Calvary.Providers;
using Calvary.ViewModels.Activities.MemberVisit;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using AutoMapper;
using Calvary.Data;
using Calvary.ViewModels;
using Calvary.Extensions.Collection;

namespace Calvary.Web.Areas.Activities.Controllers
{
    public class MemberVisitController : Controller
    {
        private MemberProvider memberProvider;
        private VisitResultProvider visitResultProvider;

        public MemberVisitController(MemberProvider memberProvider, VisitResultProvider visitResultProvider)
        {
            this.memberProvider = memberProvider;
            this.visitResultProvider = visitResultProvider;
        }

        public ActionResult Index()
        {
            var model = new IndexViewModel();            
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> arrayOfId)
        {
            try
            {
                arrayOfId.ForEach(memberProvider.DeleteMemberVisit);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }
        }

        public ActionResult VisitHistory(int id)
        {
            var member = memberProvider.GetMember(id);
            var model = new VisitHistoryViewModel();
            if (member != null)
            {
                model.MemberNo = member.MemberNo;
                model.MemberName = member.Name;
                model.Address = member.Address;
                model.MemberId = member.Id;
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Create(int memberId)
        {
            var model = new CreateEditViewModel();
            model.VisitDate = DateTime.Today;
            model.MemberId = memberId;
            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, CreateEditViewModel model)
        {
            var memberVisit = new MemberVisit();
            if (ModelState.IsValid)
            {
                Mapper.DynamicMap(model, memberVisit);
                memberProvider.AddMemberVisit(memberVisit);
            }

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new CreateEditViewModel();
            var memberVisit = memberProvider.GetMemberVisit(id);
            Mapper.DynamicMap(memberVisit, model);
            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form, CreateEditViewModel model)
        {
            var memberVisit = memberProvider.GetMemberVisit(model.Id);
            if (ModelState.IsValid)
            {
                Mapper.DynamicMap(model, memberVisit);
                memberProvider.UpdateMemberVisit(memberVisit);
            }

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }
        
        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var result = memberProvider.List();
            return Json(result.ToDataSourceResult(request));
        }


        public ActionResult ListVisitHistory([DataSourceRequest] DataSourceRequest request, int memberId)
        {
            var result = memberProvider.ListMemberVisit(memberId);
            return Json(result.ToDataSourceResult(request));
        }

        public ActionResult GetVisitResults()
        {
            var visitResults = visitResultProvider.GetVisitResults();
            return Json(visitResults, JsonRequestBehavior.AllowGet);
        }

    }
}