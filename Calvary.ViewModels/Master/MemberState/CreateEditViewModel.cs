using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calvary.ViewModels.Master.MemberState
{
    public class CreateEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Kode DKH harus diisi")]
        [Display(Name = "Kode DKH")]
        public string Code { get; set; }

        [Display(Name = "Keterangan")]
        public string Description { get; set; }

        [Display(Name = "Status Aktif")]
        public bool IsActive { get; set; }
    }
}
