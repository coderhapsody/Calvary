using System;

namespace Calvary.ViewModels.Master.Member
{
    public class ListMemberStateHistoryViewModel
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string MemberNo { get; set; }
        public string MemberName { get; set; }
        public int MemberStateId { get; set; }
        public string MemberStateDescription { get; set; }
        public DateTime EffDate { get; set; }
        public string Notes { get; set; }
    }
}
