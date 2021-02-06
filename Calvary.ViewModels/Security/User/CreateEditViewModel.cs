using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Security.User
{
    public class CreateEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nama Pengguna")]
        [Required(ErrorMessage = "Nama Pengguna harus diisi")]
        public string UserName { get; set; }

        [Display(Name = "Status Aktif")]
        public bool IsActive { get; set; }

        [Display(Name = "Grup Pengguna")]
        [Required(ErrorMessage = "Grup Pengguna harus dipilih")]
        public int RoleId { get; set; }

        [Display(Name = "Anggota")]
        public int? MemberId { get; set; }
    }
}
