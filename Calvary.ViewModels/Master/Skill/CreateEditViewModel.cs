using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Master.Skill
{
    public class CreateEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama Keahlian harus diisi")]
        [Display(Name = "Nama Keahlian")]
        public string Name { get; set; }
    }
}
