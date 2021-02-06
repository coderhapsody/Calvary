using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Master.JobGroup
{
    public partial class CreateEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama Grup Profesi harus diisi")]
        [Display(Name = "Nama Grup Profesi")]
        public string Name { get; set; }
    }
}
