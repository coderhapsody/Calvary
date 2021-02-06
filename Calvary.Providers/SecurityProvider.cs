using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Calvary.Data;
using Calvary.ViewModels.Security.MenuAccess;
using Calvary.ViewModels.Security.Role;
using Calvary.ViewModels.Security.User;

namespace Calvary.Providers
{
    public class SecurityProvider : BaseProvider
    {
        public static readonly int RoleAdministrator = 1;
        public static readonly int RoleCustomer = 2;

        public SecurityProvider(CalvaryContext context, IPrincipal principal)
            : base(context, principal)
        {
        }

        #region Roles
        public void AddRole(Role role)
        {
            if (role.Id == 0)
                context.Roles.Add(role);

            SetAuditFields(role);
            context.SaveChanges();
        }

        public void UpdateRole(Role role)
        {
            SetAuditFields(role);
            context.SaveChanges();
        }

        public IEnumerable<Role> GetRoles()
        {
            return context.Roles.ToList();
        }

        public IQueryable<ListRoleViewModel> List()
        {
            var query = from role in context.Roles
                        orderby role.ChangedWhen descending
                        select new ListRoleViewModel
                        {
                            Id = role.Id,
                            Name = role.Name
                        };
            return query;
        }

        public void UpdateRoleAccess(IList<RoleMenu> roleMenus)
        {
            if (roleMenus.Count > 0)
            {
                var firstRoleMenu = roleMenus[0];
                context.RoleMenus.RemoveRange(context.RoleMenus.Where(rm => rm.MenuId == firstRoleMenu.MenuId).ToList());

                foreach (var roleMenu in roleMenus)
                {
                    context.RoleMenus.Add(roleMenu);
                }
                context.SaveChanges();
            }

        }

        public Role GetRole(int id)
        {
            return context.Roles.SingleOrDefault(cat => cat.Id == id);
        }

        public void DeleteRole(int id)
        {
            var role = GetRole(id);
            if (role != null)
            {
                context.Roles.Remove(role);
                context.SaveChanges();
            }
        }

        public RoleMenu GetAccessPermission(string userName, int menuId)
        {
            var query = from roleMenu in context.RoleMenus
                        join userLogin in context.UserLogins on roleMenu.RoleId equals userLogin.RoleId
                        where userLogin.UserName == userName
                           && roleMenu.MenuId == menuId
                        select roleMenu;
            return query.SingleOrDefault();                   
        }
        #endregion

        #region User

        public bool ValidateUser(string userName, string password)
        {
            var user = context.UserLogins.SingleOrDefault(p => p.UserName == userName && p.Password == password);
            return user != null;
        }

        public void ChangePassword(string userName, string newPassword)
        {
            var user = context.UserLogins.SingleOrDefault(p => p.UserName == userName);
            if (user != null)
            {
                user.Password = newPassword;
                user.LastChangePassword = DateTime.Now;
                SetAuditFields(user);
                context.SaveChanges();
            }
        }
        #endregion

        public void LogInUser(string userName)
        {
            var user = context.UserLogins.SingleOrDefault(p => p.UserName == userName);
            if (user != null)
            {
                user.LastLogin = DateTime.Now;
                context.SaveChanges();
            }
        }

        #region UserLogin
        public void AddUserLogin(UserLogin userLogin)
        {
            context.UserLogins.Add(userLogin);
            SetAuditFields(userLogin);

            context.SaveChanges();
        }

        public void UpdateUserLogin(UserLogin userLogin)
        {
            SetAuditFields(userLogin);
            context.SaveChanges();
        }

        public void DeleteUserLogin(int userLoginId)
        {
            var selectedUserLogin = context.UserLogins.SingleOrDefault(userLogin => userLogin.Id == userLoginId);
            if (selectedUserLogin != null)
            {
                context.UserLogins.Remove(selectedUserLogin);
                context.SaveChanges();
            }
        }

        public IQueryable<ListUserLoginViewModel> ListUserLogins()
        {
            var query = from userLogin in context.UserLogins
                        join role in context.Roles on userLogin.RoleId equals role.Id
                        orderby userLogin.ChangedWhen descending
                        select new ListUserLoginViewModel
                        {
                            Id = userLogin.Id,
                            UserName = userLogin.UserName,
                            RoleName = role.Name,
                            LastLogin = userLogin.LastLogin,
                            LastChangePassword = userLogin.LastChangePassword,
                            IsActive = userLogin.IsActive,
                            IsSystemUser = userLogin.IsSystemUser,
                        };
            return query;
        }

        public IEnumerable<UserLogin> GetUserLogins()
        {
            var query = context.UserLogins;
            return query.ToList();
        }

        public UserLogin GetUserLogin(int userLoginId)
        {
            var selectedUserLogin = context.UserLogins.SingleOrDefault(userLogin => userLogin.Id == userLoginId);
            return selectedUserLogin;
        }

        public UserLogin GetUserLogin(string userName)
        {
            var selectedUserLogin = context.UserLogins.SingleOrDefault(userLogin => userLogin.UserName == userName);
            return selectedUserLogin;
        }
        #endregion

        public IEnumerable<ListMenuViewModel> ListMenus(int? parentMenuId)
        {
            var userLogin = GetUserLogin(principal.Identity.Name);
            
            var query = from menu in context.Menus
                        from role in menu.RoleMenus
                        where menu.IsActive && menu.ParentMenuId == parentMenuId && role.RoleId == userLogin.RoleId
                        orderby menu.Seq
                        select new ListMenuViewModel
                        {
                            id = menu.Id,
                            Name = menu.Title,
                            Url = menu.NavigationTo,
                            hasChildren = menu.Menus.Any()
                        };
            return query.ToList();
        }

        public IEnumerable<ListMenuViewModel> ListAllMenus(int? parentMenuId)
        {
            var query = from menu in context.Menus
                        where menu.IsActive && menu.ParentMenuId == parentMenuId
                        orderby menu.Seq
                        select new ListMenuViewModel
                        {
                            id = menu.Id,
                            Name = menu.Title,
                            Url = menu.NavigationTo,
                            hasChildren = menu.Menus.Any()
                        };
            return query.ToList();
        }

        public IEnumerable<dynamic> GetRolesFromMenu(int menuId)
        {
            foreach (var role in context.Roles)
            {
                var roleMenu = context.RoleMenus.SingleOrDefault(rm => rm.RoleId == role.Id && rm.MenuId == menuId);
                if (roleMenu != null)
                {
                    yield return new
                    {
                        MenuId = menuId,
                        RoleId = role.Id,
                        RoleName = role.Name,
                        AllowRole = true,
                        AllowAdd = roleMenu.AllowAdd,
                        AllowEdit = roleMenu.AllowEdit,
                        AllowDelete = roleMenu.AllowDelete,
                    };
                }
                else
                {
                    yield return new
                    {
                        MenuId = menuId,
                        RoleId = role.Id,
                        RoleName = role.Name,
                        AllowRole = false,
                        AllowAdd = false,
                        AllowEdit = false,
                        AllowDelete = false,
                    };
                }
            }
        }
    }
}
