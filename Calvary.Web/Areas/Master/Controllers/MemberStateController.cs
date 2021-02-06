using Calvary.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Calvary.ViewModels.Master.MemberState;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Calvary.Data;
using AutoMapper;
using Calvary.ViewModels;

namespace Calvary.Web.Areas.Master.Controllers
{
    public class MemberStateController : Controller
    {
        private MemberProvider memberProvider;
        public MemberStateController(MemberProvider memberProvider)
        {
            this.memberProvider = memberProvider;
        }

        // GET: Master/MemberState
        public ActionResult Index()
        {
            var model = new IndexViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CreateEditViewModel();
            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Create(FormCollection form, CreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var memberState = new MemberState();
                Mapper.DynamicMap(model, memberState);
                memberProvider.AddMemberState(memberState);
            }
            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new CreateEditViewModel();
            var memberState = memberProvider.GetMemberState(id);
            Mapper.DynamicMap(memberState, model);
            model.Description = Server.HtmlDecode(model.Description);
            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form, CreateEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var memberState = memberProvider.GetMemberState(model.Id);
                Mapper.DynamicMap(model, memberState);
                memberProvider.UpdateMemberState(memberState);
            }
            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var list = memberProvider.ListMemberState();
            return Json(list.ToDataSourceResult(request));
        }
    }
}