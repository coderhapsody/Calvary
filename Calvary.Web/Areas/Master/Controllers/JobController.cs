using System;
using System.Web.Mvc;
using AutoMapper;
using Calvary.Data;
using Calvary.Providers;
using Calvary.ViewModels;
using Calvary.ViewModels.Master.Job;
using Calvary.Web.Base;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Calvary.Web.Areas.Master.Controllers
{
    public class JobController : BaseController
    {
        private readonly JobProvider jobProvider;
        public JobController(JobProvider jobProvider)
        {
            this.jobProvider = jobProvider;
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
            var job = jobProvider.GetJob(id);
            Mapper.DynamicMap(job, model);

            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            var job = jobProvider.GetJob(model.Id);
            Mapper.DynamicMap(model, job);
            jobProvider.UpdateJob(job);

            var jsonViewModel = new AjaxViewModel(true, null, null);
            return Json(jsonViewModel);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            var job = new Job();
            Mapper.DynamicMap(model, job);
            jobProvider.AddJob(job);

            var jsonViewModel = new AjaxViewModel(true, null, null);
            return Json(jsonViewModel);
        }

        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var list = jobProvider.List();
            return Json(list.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Delete(int[] arrayOfId)
        {
            try
            {
                Array.ForEach(arrayOfId, jobProvider.DeleteJob);
                return Json(new AjaxViewModel(true, null, null));
            }
            catch (Exception ex)
            {
                return Json(new AjaxViewModel(false, null, ex.Message));
            }
        }
    }
}