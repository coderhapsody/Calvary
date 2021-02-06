using System;

namespace Calvary.ViewModels.Activities.Resignation
{
    public class ListResignationViewModel
    {
        public int Id { get; set; }
        public string MemberNo { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public int? DestShrineId { get; set; }
        public string DestShrineName { get; set; }
        public DateTime? ResignDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
