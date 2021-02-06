using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calvary.ViewModels.Activities.Worship
{
    public class ListIncomeViewModel
    {
        public int Id { get; set; }
        public string IncomeTypeName { get; set; }
        public string WorshipName { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string ReceivedBy { get; set; }
        public decimal Amount { get; set; }
    }
}
