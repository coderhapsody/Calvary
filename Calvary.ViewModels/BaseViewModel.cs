using System;

namespace Calvary.ViewModels
{
    public abstract class BaseViewModel : BaseConcurrencyViewModel
    {
        public DateTime CreatedWhen { get; set; }
        public DateTime ChangedWhen { get; set; }
        public string CreatedBy { get; set; }
        public string ChangedBy { get; set; }
    }
}
