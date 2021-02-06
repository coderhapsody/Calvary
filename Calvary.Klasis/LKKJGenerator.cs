using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Calvary.Data;

namespace Calvary.Klasis
{
    public class LKKJGenerator : BaseKlasisGenerator, IKlasisGenerator
    {
        public LKKJGenerator(CalvaryContext context, IPrincipal principal) : base(context, principal)
        {
        }

        public async Task Generate(int fromYear, int toYear)
        {
            throw new NotImplementedException();
        }

        public void SetInputOutputFile(string sourceExcelFile, string destExcelFile)
        {
            throw new NotImplementedException();
        }
    }
}
