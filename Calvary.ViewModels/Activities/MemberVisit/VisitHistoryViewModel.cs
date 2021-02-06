using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Activities.MemberVisit
{
    public class VisitHistoryViewModel
    {
        public int MemberId { get; set; }
        [Display(Name = "No. Anggota")]
        public string MemberNo { get; set; }
        [Display(Name = "Nama Anggota")]
        public string MemberName { get; set; }
        [Display(Name = "Alamat")]
        public string Address { get; set; }
        public IEnumerable<ListMemberVisitViewModel> List { get; set; }
    }
}
