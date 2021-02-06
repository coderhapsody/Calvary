using AutoMapper;
using Calvary.Data;
using Calvary.Providers;
using Calvary.ViewModels;
using Calvary.ViewModels.Activities.Schedule;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Calvary.Web.Areas.Activities.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ScheduleProvider scheduleProvider;

        public ScheduleController(ScheduleProvider scheduleProvider)
        {
            this.scheduleProvider = scheduleProvider;
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
            var scheduleGroup = scheduleProvider.GetScheduleGroup(id);
            Mapper.DynamicMap(scheduleGroup, model);

            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            var schedule = new Schedule();
            Mapper.DynamicMap(model, schedule);
            scheduleProvider.UpdateSchedule(schedule);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            var schedule = new Schedule();
            Mapper.DynamicMap(model, schedule);
            scheduleProvider.AddSchedule(schedule);

            var jsonViewModel = new AjaxViewModel(true, model, null);
            return Json(jsonViewModel);
        }

        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var list = scheduleProvider.ListSchedules();
            return Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> arrayOfId)
        {
            try
            {
                Array.ForEach(arrayOfId.ToArray(), scheduleProvider.DeleteSchedule);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }
        }


    }
}