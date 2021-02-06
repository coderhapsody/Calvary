using Calvary.Klasis;
using Calvary.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Calvary.Web.Controllers
{
    public class ReportController : Controller
    {
        private const string klasisDBAJtemplate = "TemplateDBAJ.xls";

        private readonly DBAJGenerator klasisDBAJGenerator;
        private readonly LookUpProvider lookUpProvider;

        public ReportController(DBAJGenerator klasisDBAJGenerator, LookUpProvider lookUpProvider)
        {
            this.klasisDBAJGenerator = klasisDBAJGenerator;
            this.lookUpProvider = lookUpProvider;
        }

        public ActionResult Resignation()
        {
            return View();
        }

        public ActionResult Deceased()
        {
            return View();
        }

        public ActionResult Birthday()
        {
            return View();
        }

        public ActionResult MemberInfo()
        {
            return View();
        }

        public ActionResult PenerimaanUang()
        {
            return View();
        }

        public ActionResult Pernikahan()
        {
            return View();
        }

        public ActionResult KlasisLKKJ()
        {
            ViewBag.FlagUmum = lookUpProvider.GetLookUpValues("FlagUmum");
            return View();
        }

        public ActionResult GolonganDarah()
        {
            ViewBag.GolDarah = lookUpProvider.GetLookUpValues("BloodType");
            return View();
        }

        public ActionResult RingkasanIbadah()
        {
            return View();
        }

        public ActionResult RingkasanPenerimaanUang()
        {
            return View();
        }

        #region Klasis DBAJ
        [HttpGet]
        public ActionResult KlasisDBAJ()
        {
            int fromYear, toYear;

            if (DateTime.Today.Month > 4)
                fromYear = DateTime.Today.Year;
            else
                fromYear = DateTime.Today.Year - 1;

            toYear = fromYear + 1;

            ViewBag.FromYear = fromYear;
            ViewBag.ToYear = toYear;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> KlasisDBAJ(int fromYear, int toYear)
        {
            string source = $"{Server.MapPath("~/ReportTemplates/")}{klasisDBAJtemplate}";

            string generatedFileName = $"{DateTime.Now.ToString("ReportDBAJ_yyyyMMddHHmmss")}.xls";
            string dest = Server.MapPath($"~/TempOutput/{generatedFileName}");
            klasisDBAJGenerator.SetInputOutputFile(source, dest);
            await klasisDBAJGenerator.Generate(fromYear, toYear);

            byte[] fileData = System.IO.File.ReadAllBytes(dest);
            var contentDisposition = new System.Net.Mime.ContentDisposition
            {
                FileName = generatedFileName,
                Inline = false,
            };
            Response.AppendHeader("Content-Disposition", contentDisposition.ToString());
            return File(fileData, "application/force-download");

        }
        #endregion
    }
}