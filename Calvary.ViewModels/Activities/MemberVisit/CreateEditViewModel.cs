using System;
using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Activities.MemberVisit
{
    public class CreateEditViewModel
    {
        public int Id { get; set; }
        public int MemberId { get; set; }

        [Display(Name = "Tanggal Pelawatan")]
        [Required(ErrorMessage = "Tanggal Pelawatan harus dipilih")]
        public DateTime VisitDate { get; set; }

        [Display(Name = "Hasil Pelawatan")]
        [Required(ErrorMessage = "Hasil Pelawatan harus dipilih")]
        public int VisitResultId { get; set; }

        [Display(Name = "Catatan")]
        public string Notes { get; set; }
    }
}
