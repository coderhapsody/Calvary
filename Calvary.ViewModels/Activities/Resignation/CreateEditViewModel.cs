using System;
using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Activities.Resignation
{
    public class CreateEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nama Anggota")]
        public int MemberId { get; set; }

        [Required(ErrorMessage = "Nomor Anggota harus dipilih")]
        public string MemberNo { get; set; }

        [Display(Name = "Nama Anggota")]
        [Required(ErrorMessage = "Nama Anggota harus dipilih")]
        public string MemberName { get; set; }

        [Display(Name = "Gereja Tujuan")]
        [Required(ErrorMessage = "Gereja Tujuan harus dipilih")]
        public int ShrineId { get; set; }

        [Display(Name = "Gereja Tujuan")]
        public string ShrineName { get; set; }

        [Display(Name="Tgl. Atestasi")]
        public DateTime? ResignDate { get; set; }

        [Display(Name = "Alamat")]
        public string Address { get; set; }

        [Display(Name = "Kota")]
        public string City { get; set; }
    }
}
