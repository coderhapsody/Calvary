using System;

namespace Calvary.ViewModels.Activities.Schedule
{
    public class ListScheduleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int ScheduleGroupId { get; set; }
        public string ScheduleGroupName { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
    }
}
