using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calvary.ViewModels.Activities.Schedule;

namespace Calvary.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public IEnumerable<ListScheduleViewModel> Schedules { get; set; }
        public IEnumerable<BirthdayViewModel> Birthdays { get; set; }
    }
}
