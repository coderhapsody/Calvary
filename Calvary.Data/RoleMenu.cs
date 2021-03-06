// <auto-generated>
// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
// TargetFrameworkVersion = 4.5
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Threading;
using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

namespace Calvary.Data
{
    // RoleMenu
    public partial class RoleMenu
    {
        public int RoleId { get; set; } // RoleID (Primary key)
        public int MenuId { get; set; } // MenuID (Primary key)
        public bool AllowAdd { get; set; } // AllowAdd
        public bool AllowEdit { get; set; } // AllowEdit
        public bool AllowDelete { get; set; } // AllowDelete

        // Foreign keys
        public virtual Menu Menu { get; set; } // FK_RoleMenu_Menu
        public virtual Role Role { get; set; } // FK_RoleMenu_Role
        
        public RoleMenu()
        {
            AllowAdd = false;
            AllowEdit = false;
            AllowDelete = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
