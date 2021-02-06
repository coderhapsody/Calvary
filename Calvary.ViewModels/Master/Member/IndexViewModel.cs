using System.Collections.Generic;

namespace Calvary.ViewModels.Master.Member
{
    public class IndexViewModel
    {
        public bool IncludeDeceased { get; set; } = true;
        public bool IncludeResigned { get; set; } = true;
        public IEnumerable<ListMemberViewModel> List { get; set; }
    }
}
