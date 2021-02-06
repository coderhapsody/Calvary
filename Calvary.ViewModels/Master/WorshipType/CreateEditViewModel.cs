using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Master.WorshipType
{
    public class CreateEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Jenis Ibadah harus diisi")]
        [Display(Name = "Jenis Ibadah")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Harus pilih Umum atau NonUmum")]
        [Display(Name = "Umum/NonUmum")]
        public string FlagUmum { get; set; }

        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
    }
}
