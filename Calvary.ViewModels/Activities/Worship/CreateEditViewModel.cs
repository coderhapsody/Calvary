using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calvary.ViewModels.Activities.Worship
{
    public class CreateEditViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Tanggal Ibadah")]
        public DateTime Date { get; set; }
        [Display(Name = "Jenis Ibadah")]
        public int WorshipTypeId { get; set; }
        [Required]
        [Display(Name = "Tema Ibadah")]
        public string Subject { get; set; }
        [Display(Name = "Pembicara")]
        public string Priest { get; set; }
        public int JemaatPria { get; set; }
        public int JemaatWanita { get; set; }
        public int SimpatisanPria { get; set; }
        public int SimpatisanWanita { get; set; }
        public int PenatuaPria { get; set; }
        public int PenatuaWanita { get; set; }
        public int PemusikPria { get; set; }
        public int PemusikWanita { get; set; }

        public int AktivisPria { get; set; }
        public int AktivisWanita { get; set; }

        public string Notes { get; set; }
    }
}
