using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Master.EducationGrade
{
    public class CreateEditViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama Pendidikan harus diisi")]
        [Display(Name = "Nama Pendidikan")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Urutan tampilan")]
        [Display(Name = "Urutan tampilan")]
        public int Seq { get; set; }
    }
}
