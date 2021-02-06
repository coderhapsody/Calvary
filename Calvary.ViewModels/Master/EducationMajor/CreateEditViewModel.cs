using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Master.EducationMajor
{
    public class CreateEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama Bidang Pendidikan harus diisi")]
        [Display(Name = "Nama Bidang Pendidikan")]
        public string Name { get; set; }
    }
}
