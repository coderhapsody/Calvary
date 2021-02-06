using System;

namespace Calvary.ViewModels.Security.User
{
    public class ListUserLoginViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }        
        public DateTime? LastLogin { get; set; }
        public DateTime? LastChangePassword { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; }
        public bool IsSystemUser { get; set; }        
    }
}
