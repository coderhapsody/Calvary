using System;

namespace Calvary.ViewModels.Master.Member
{
    public class MemberNoteViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string Notes { get; set; }
    }
}
