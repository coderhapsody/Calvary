using System;

namespace Calvary.ViewModels.Master.Member
{
    public class MemberMarriageViewModel
    {
        public int Id { get; set; }
        public string MarriedToMemberNo { get; set; }        
        public string SpouseName { get; set; }
        public DateTime? MarriageDate { get; set; }
        public string ShrineName { get; set; }
        public string Priest { get; set; }
        public string Notes { get; set; }
    }
}
