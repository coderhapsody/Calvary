using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calvary.ViewModels.Security.MenuAccess
{
    public class RoleAccessViewModel
    {
        public int MenuId { get; set; }
        public int RoleId { get; set; }
        public bool AllowAdd { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowDelete { get; set; }
    }
}
