using Calvary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Calvary.Providers
{
    public class VisitResultProvider : BaseProvider
    {        
        public VisitResultProvider(CalvaryContext context, IPrincipal principal) : base(context, principal)
        {
        }

        public IEnumerable<VisitResult> GetVisitResults()
        {
            context.Configuration.ProxyCreationEnabled = false;
            return context.VisitResults.ToList();
        }


    }
}
