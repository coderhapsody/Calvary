using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Master.Hobby
{
    public class CreateEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama Hobby harus diisi")]
        [Display(Name = "Nama Hobby")]
        public string Name { get; set; }
    }
}
