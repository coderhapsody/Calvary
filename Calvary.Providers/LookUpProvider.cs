using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Calvary.Data;

namespace Calvary.Providers
{
    public class LookUpProvider : BaseProvider
    {
        public LookUpProvider(CalvaryContext context, IPrincipal principal) : base(context, principal)
        {
        }

        public IEnumerable<string> GetLookUpValues(string key)
        {
            LookUp lookUp = context.LookUps.SingleOrDefault(l => l.Key == key);
            if(lookUp != null)
            {
                return lookUp.Values.Split(';');
            }

            return new string[] { };
        }
    }
}
