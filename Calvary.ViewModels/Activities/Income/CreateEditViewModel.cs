using System;
using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Activities.Income
{
    public class CreateEditViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Jenis Persembahan")]
        [Required(ErrorMessage = "Jenis Persembahan harus dipilih")]
        public int IncomeTypeId { get; set; }

        [Display(Name = "Diterima Tanggal")]
        public DateTime ReceivedDate { get; set; }

        [Display(Name = "Diterima Oleh")]
        public string ReceivedBy { get; set; }

        [Display(Name = "Mata Uang")]
        [Required(ErrorMessage = "Mata Uang harus dipilih")]
        public string Currency { get; set; }

        [Display(Name = "Nominal")]
        public decimal Amount { get; set; }

        [Display(Name = "Catatan")]
        public string Notes { get; set; }
    }
}
