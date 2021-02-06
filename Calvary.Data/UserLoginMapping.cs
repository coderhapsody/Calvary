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
    // UserLogin
    public partial class UserLoginMapping : EntityTypeConfiguration<UserLogin>
    {
        public UserLoginMapping()
            : this("dbo")
        {
        }
 
        public UserLoginMapping(string schema)
        {
            ToTable(schema + ".UserLogin");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserName).HasColumnName("UserName").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.Password).HasColumnName("Password").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(200);
            Property(x => x.RoleId).HasColumnName("RoleId").IsRequired().HasColumnType("int");
            Property(x => x.LastLogin).HasColumnName("LastLogin").IsOptional().HasColumnType("datetime");
            Property(x => x.LastChangePassword).HasColumnName("LastChangePassword").IsOptional().HasColumnType("datetime");
            Property(x => x.MemberId).HasColumnName("MemberId").IsOptional().HasColumnType("int");
            Property(x => x.IsSystemUser).HasColumnName("IsSystemUser").IsRequired().HasColumnType("bit");
            Property(x => x.IsActive).HasColumnName("IsActive").IsRequired().HasColumnType("bit");
            Property(x => x.CreatedWhen).HasColumnName("CreatedWhen").IsRequired().HasColumnType("datetime");
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.ChangedWhen).HasColumnName("ChangedWhen").IsRequired().HasColumnType("datetime");
            Property(x => x.ChangedBy).HasColumnName("ChangedBy").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);

            // Foreign keys
            HasOptional(a => a.Member).WithMany(b => b.UserLogins).HasForeignKey(c => c.MemberId); // FK_UserLogin_Member
            HasRequired(a => a.Role).WithMany(b => b.UserLogins).HasForeignKey(c => c.RoleId); // FK_UserLogin_Role
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
