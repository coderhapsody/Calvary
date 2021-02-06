using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calvary.ViewModels.Activities.Income
{
    public class ListIncomeViewModel
    {
        public int Id { get; set; }

        public string IncomeTypeName { get; set; }

        public string WorshipName { get; set; }

        public DateTime ReceivedDate { get; set; }

        public string ReceivedBy { get; set; }

        [Display(Name = "Persembahan (IDR)")]
        public decimal Amount { get; set; }

        public string Notes { get; set; }
    }
}
