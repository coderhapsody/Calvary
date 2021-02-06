using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calvary.ViewModels.Activities.Worship
{
    public class IncomeIndexViewModel
    {
        public Data.Worship SelectedWorship { get; set; }
        public IEnumerable<ListIncomeViewModel> List { get; set; }

    }
}
