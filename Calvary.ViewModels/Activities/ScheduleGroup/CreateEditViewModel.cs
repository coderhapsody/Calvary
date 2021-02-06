using System.ComponentModel.DataAnnotations;

namespace Calvary.ViewModels.Activities.ScheduleGroup
{
    public partial class CreateEditViewModel : BaseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nama Grup Kegiatan harus diisi")]
        [Display(Name = "Nama Grup Kegiatan")]
        public string Name { get; set; }

        [Display(Name = "Status Aktif")]
        public bool IsActive { get; set; }
    }
}
