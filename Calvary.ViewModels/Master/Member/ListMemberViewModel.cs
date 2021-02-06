using System;

namespace Calvary.ViewModels.Master.Member
{
    public class ListMemberViewModel
    {
        public int Id { get; set; } //(int, not null)
        public string MemberNo { get; set; } //(varchar(20), not null)
        public string Name { get; set; } //(varchar(100), not null)
        public string NickName { get; set; } //(varchar(50), not null)
        public string Gender { get; set; } //(varchar(1), not null)
        public string HomePhone { get; set; } //(varchar(20), null)
        public string CellPhone1 { get; set; } //(varchar(20), null)
        public string Email { get; set; } //(varchar(100), null)
        public DateTime? BirthDate { get; set; } //(date, null)
        public DateTime ChangedWhen { get; set; }
    }
}
