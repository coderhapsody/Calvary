using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Master.Job
{
    public class CreateEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama Profesi harus diisi")]
        [Display(Name = "Nama Profesi")]
        public string Name { get; set; }

        [Display(Name = "Grup Profesi")]
        public int? JobGroupId { get; set; }
    }
}
