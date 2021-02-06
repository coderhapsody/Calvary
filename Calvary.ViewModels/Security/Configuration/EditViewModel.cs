using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Security.Configuration
{
    public class EditViewModel
    {
        [Required]
        [Display(Name="Nama Gereja")]
        public string NamaGereja { get; set; }

        [Required]
        [Display(Name = "Kota Gereja")]
        public string KotaGereja { get; set; }

        [Required]
        [Display(Name = "PIC LKKJ")]
        public string PICLKKJ { get; set; }

        [Required]
        [Display(Name = "Ketua Umum")]
        public string KetuaUmum { get; set; }

        [Required]
        [Display(Name = "Sekretaris Umum")]
        public string SekretarisUmum { get; set; }
    }
}
