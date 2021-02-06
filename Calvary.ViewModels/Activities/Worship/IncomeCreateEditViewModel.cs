using System;

namespace Calvary.ViewModels.Activities.Worship
{
    public class IncomeCreateEditViewModel
    {
        public int Id { get; set; }
        public int IncomeTypeId { get; set; }
        public int WorshipId { get; set; }        
        public DateTime ReceivedDate { get; set; }
        public string ReceivedBy { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
    }
}
