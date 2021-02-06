using Calvary.Providers;
using Calvary.ViewModels.Security.Configuration;
using Calvary.Web.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Calvary.Web.Areas.Security.Controllers
{
    public class ConfigurationController : BaseController
    {
        public ConfigurationController() 
        {                
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new EditViewModel();
            viewModel.NamaGereja = ConfigurationInstance.GetValue(ConfigurationKeys.NamaGereja);
            viewModel.KotaGereja = ConfigurationInstance.GetValue(ConfigurationKeys.KotaGereja);
            viewModel.PICLKKJ = ConfigurationInstance.GetValue(ConfigurationKeys.PICLKKJ);
            viewModel.KetuaUmum = ConfigurationInstance.GetValue(ConfigurationKeys.KetuaUmum);
            viewModel.SekretarisUmum = ConfigurationInstance.GetValue(ConfigurationKeys.SekretarisUmum);
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(EditViewModel viewModel)
        {
            ConfigurationInstance.SetValue(ConfigurationKeys.NamaGereja, viewModel.NamaGereja);
            ConfigurationInstance.SetValue(ConfigurationKeys.PICLKKJ, viewModel.PICLKKJ);
            ConfigurationInstance.SetValue(ConfigurationKeys.KotaGereja, viewModel.KotaGereja);
            ConfigurationInstance.SetValue(ConfigurationKeys.KetuaUmum, viewModel.KetuaUmum);
            ConfigurationInstance.SetValue(ConfigurationKeys.SekretarisUmum, viewModel.SekretarisUmum);
            return RedirectToAction("Index");
        }
    }
}