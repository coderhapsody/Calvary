using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Master.Ethnic
{
    public partial class CreateEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama Etnis harus diisi")]
        [Display(Name = "Nama Etnis")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Nama Etnis di Klasis harus diisi")]
        [Display(Name = "Nama Etnis di Klasis")]
        public string KlasisMappingValue { get; set; }
    }
}
