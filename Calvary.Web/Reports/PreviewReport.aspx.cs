using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Calvary.Web.Reports
{
    public partial class PreviewReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                rptReport.InteractivityPostBackMode = InteractivityPostBackMode.AlwaysAsynchronous;
                string reportName = Request.QueryString["RDL"];
                var keys = Request.QueryString.AllKeys.Where(param => param != "RDL");

                var parameters = new List<ReportParameter>();
                foreach (string key in keys)
                    parameters.Add(new ReportParameter(key, Request.QueryString[key]));               

                ShowReport(reportName, parameters);
            }
        }

        public void ShowReport(string reportName, List<ReportParameter> parameters)
        {
            //rptReport.ServerReport.ReportServerCredentials = new ReportServerCredentials(reportServerUserName, reportServerPassword, reportServerDomain);
            rptReport.Visible = true;
            rptReport.ProcessingMode = ProcessingMode.Remote;
            rptReport.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServer"]);
            rptReport.ServerReport.ReportPath = String.Format(@"/{0}/{1}", ConfigurationManager.AppSettings["ReportFolder"], reportName);
            rptReport.ServerReport.SetParameters(parameters);
            rptReport.ServerReport.Refresh();
        }
    }
}