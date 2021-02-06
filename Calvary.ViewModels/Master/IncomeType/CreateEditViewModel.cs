using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Master.IncomeType
{
    public class CreateEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Jenis Persembahan harus diisi")]
        [Display(Name = "Jenis Persembahan")]
        public string Name { get; set; }

        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
    }
}
