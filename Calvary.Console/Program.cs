using Calvary.Klasis;
using Calvary.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Calvary.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Calvary.Data.CalvaryContext("name=CalvaryConnection"))
            {
                IIdentity identity = new GenericIdentity("testing");
                IPrincipal principal = new GenericPrincipal(identity, null);
                ConfigurationProvider config = new ConfigurationProvider(context, principal);
                DBAJGenerator dbaj = new DBAJGenerator(context, principal, config);

                string source = @"D:\src\Calvary\Calvary.Web\ReportTemplates\TemplateDBAJ.xls";
                dbaj.SetInputOutputFile(source, @"d:\temp\dbaj.xls");
                dbaj.Generate(2011, 2012);

            }
        }
    }
}
