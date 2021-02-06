using Calvary.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calvary.Data;
using System.Security.Principal;

namespace Calvary.Klasis
{
    public abstract class BaseKlasisGenerator : BaseProvider
    {
        public BaseKlasisGenerator(CalvaryContext context, IPrincipal principal) 
            : base(context, principal)
        {
        }
    }
}
