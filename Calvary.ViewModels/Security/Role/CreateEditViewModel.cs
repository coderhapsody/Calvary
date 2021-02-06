using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Security.Role
{
    public class CreateEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama Grup Pengguna harus diisi")]
        [Display(Name = "Nama Grup Pengguna")]
        public string Name { get; set; }
    }
}
