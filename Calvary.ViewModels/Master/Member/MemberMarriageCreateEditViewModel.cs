using System;
using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Master.Member
{
    public class MemberMarriageCreateEditViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public int MemberId { get; set; }

        public bool MarriedToMember { get; set; }

        public int SpouseId { get; set; }

        [Display(Name = "Nomor Anggota")]
        public string SpouseNo { get; set; }

        [Display(Name = "Tanggal Nikah")]
        public DateTime MarriageDate { get; set; }

        [Display(Name = "Nama Pasangan")]
        public string SpouseName { get; set; }

        [Display(Name = "Diberkati oleh")]
        public string Priest { get; set; }

        [Display(Name = "Kota")]
        public string City { get; set; }

        [Display(Name = "Diberkati di")]
        public int? ShrineId { get; set; }

        [Display(Name = "Nomor Cat. Sipil")]
        public string LegalRegNo { get; set; }

        [Display(Name = "Nomor Akte Gereja")]
        public string ChurchRegNo { get; set; }

        [Display(Name = "Tanggal Cat. Sipil")]
        public DateTime? LegalDate { get; set; }

    }
}
