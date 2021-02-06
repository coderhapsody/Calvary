using System;

namespace Calvary.ViewModels.Master.Member
{
    public class MemberStateHistoryCreateEditViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int MemberStateId { get; set; }        
        public DateTime EffDate { get; set; }
        public string Notes { get; set; }
    }
}
