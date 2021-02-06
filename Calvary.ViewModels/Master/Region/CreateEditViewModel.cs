using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Calvary.ViewModels.Master.Region
{
    public class CreateEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kode Wilayah harus diisi")]
        [Display(Name = "Kode Wilayah")]
        [Remote("ValidateCode", "Region", "Master", AdditionalFields = "Id", ErrorMessage = "Kode Wilayah sudah digunakan")] 
        public string Code { get; set; }

        [Required(ErrorMessage = "Nama Wilayah harus diisi")]
        [Display(Name = "Nama Wilayah")]
        public string Name { get; set; }

        [Display(Name = "Status Aktif")]
        public bool IsActive { get; set; }
    }
}
