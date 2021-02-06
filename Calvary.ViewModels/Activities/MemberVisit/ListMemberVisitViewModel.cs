using System;

namespace Calvary.ViewModels.Activities.MemberVisit
{
    public class ListMemberVisitViewModel
    {
        public int Id { get; set; }
        public string MemberNo { get; set; }
        public string MemberName { get; set; }
        public DateTime VisitDate { get; set; }
        public int VisitResultId { get; set; }
        public string VisitResultName { get; set; }
        public string Notes { get; set; }
    }
}
