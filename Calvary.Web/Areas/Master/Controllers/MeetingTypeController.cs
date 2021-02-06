using Calvary.Providers;
using Calvary.Web.Base;
using Calvary.ViewModels.Master.MeetingType;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Calvary.ViewModels;
using Calvary.Data;
using Kendo.Mvc.Extensions;
using Calvary.Extensions.Collection;

namespace Calvary.Web.Areas.Master.Controllers
{
    public class MeetingTypeController : BaseController
    {
        private MeetingTypeProvider meetingTypeProvider;

        public MeetingTypeController(MeetingTypeProvider meetingTypeProvider)
        {
            this.meetingTypeProvider = meetingTypeProvider;
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
            var meetingType = meetingTypeProvider.GetMeetingType(id);
            Mapper.DynamicMap(meetingType, model);

            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            var hobby = meetingTypeProvider.GetMeetingType(model.Id);
            Mapper.DynamicMap(model, hobby);
            meetingTypeProvider.UpdateMeetingType(hobby);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            var meetingType = new MeetingType();
            Mapper.DynamicMap(model, meetingType);
            meetingTypeProvider.AddMeetingType(meetingType);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var list = meetingTypeProvider.List();
            return Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> arrayOfId)
        {
            try
            {
                arrayOfId.ForEach(meetingTypeProvider.DeleteMeetingType);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }
        }

    }
}