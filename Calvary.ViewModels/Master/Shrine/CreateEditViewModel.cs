using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Master.Shrine
{
    public class CreateEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama Tempat Ibadah harus diisi")]
        [Display(Name = "Nama Tempat Ibadah")]
        public string Name { get; set; }

        [Display(Name = "Alamat")]
        [Required(ErrorMessage = "Alamat harus diisi")]
        public string Address { get; set; }

        [Display(Name = "Kota")]
        public string City { get; set; }

        [Display(Name = "Telepon")]
        public string Phone { get; set; }
    }
}
