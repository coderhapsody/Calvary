using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calvary.ViewModels.Master.MeetingType
{
    public class CreateEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Jenis Ibadah harus diisi")]
        [Display(Name = "Jenis Ibadah")]
        public string Name { get; set; }


        [Display(Name = "Aktif")]
        public bool IsActive { get; set; }
    }
}
