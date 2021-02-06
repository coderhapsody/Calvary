using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calvary.ViewModels.Activities.Schedule
{
    public class CreateEditViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int ScheduleGroupId { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
    }
}
