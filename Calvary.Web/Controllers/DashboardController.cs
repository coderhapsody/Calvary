using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using Calvary.Data.ProcedureModels;
using Calvary.Providers;
using Calvary.ViewModels.Dashboard;
using Calvary.Web.Base;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Calvary.Web.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly DashboardProvider dashboardProvider;

        public DashboardController(DashboardProvider dashboardProvider)
        {
            this.dashboardProvider = dashboardProvider;
        }

        public async Task<ActionResult> TodayBirthday([DataSourceRequest] DataSourceRequest request)
        {
            var birthdays = await dashboardProvider.GetTodayBirthday();
            var model = new List<BirthdayViewModel>();
            Mapper.CreateMap<ProcReportUlangTahunModel, BirthdayViewModel>();
            Mapper.Map(birthdays, model);
            return Json(model.ToDataSourceResult(request));
        }

        public async Task<ActionResult> MemberSummaryByGender()
        {
            var result = await dashboardProvider.GetMemberSummaryByGender();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> MemberSummaryByAge()
        {
            var result = await dashboardProvider.GetMemberSummaryByAge();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}