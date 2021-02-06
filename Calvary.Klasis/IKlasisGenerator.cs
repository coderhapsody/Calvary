using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calvary.Klasis
{
    interface IKlasisGenerator
    {
        Task Generate(int fromYear, int toYear);
        void SetInputOutputFile(string sourceExcelFile, string destExcelFile);
    }
}
