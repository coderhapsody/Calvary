using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calvary.ViewModels
{
    public class BaseConcurrencyViewModel
    {
        public byte[] RowVersion { get; set; }
    }
}
