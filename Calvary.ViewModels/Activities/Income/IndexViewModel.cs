using System.Collections.Generic;

namespace Calvary.ViewModels.Activities.Income
{
    public class IndexViewModel
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public IEnumerable<ListIncomeViewModel> List { get; set; }
    }
}
