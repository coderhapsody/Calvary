using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "Password Lama harus diisi")]
        [Display(Name = "Password Lama")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Password Baru harus diisi")]
        [Display(Name = "Password Baru")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Konfirmasi Password Baru harus diisi")]
        [Compare("NewPassword", ErrorMessage = "Konfirmasi Password Baru salah")]
        [Display(Name = "Konfirmasi Password Baru")]
        public string ConfirmPassword { get; set; }
    }
}
