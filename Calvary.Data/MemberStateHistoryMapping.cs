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
    // MemberStateHistory
    public partial class MemberStateHistoryMapping : EntityTypeConfiguration<MemberStateHistory>
    {
        public MemberStateHistoryMapping()
            : this("dbo")
        {
        }
 
        public MemberStateHistoryMapping(string schema)
        {
            ToTable(schema + ".MemberStateHistory");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.MemberId).HasColumnName("MemberId").IsRequired().HasColumnType("int");
            Property(x => x.MemberStateId).HasColumnName("MemberStateId").IsRequired().HasColumnType("int");
            Property(x => x.EffDate).HasColumnName("EffDate").IsRequired().HasColumnType("date");
            Property(x => x.Notes).HasColumnName("Notes").IsOptional().IsUnicode(false).HasColumnType("text").HasMaxLength(2147483647);
            Property(x => x.CreatedWhen).HasColumnName("CreatedWhen").IsRequired().HasColumnType("datetime");
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.ChangedWhen).HasColumnName("ChangedWhen").IsRequired().HasColumnType("datetime");
            Property(x => x.ChangedBy).HasColumnName("ChangedBy").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.RowVersion).HasColumnName("RowVersion").IsRequired().IsFixedLength().HasColumnType("timestamp").HasMaxLength(8).IsRowVersion().IsConcurrencyToken().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            // Foreign keys
            HasRequired(a => a.Member).WithMany(b => b.MemberStateHistories).HasForeignKey(c => c.MemberId); // FK_MemberStatusHistory_Member
            HasRequired(a => a.MemberState).WithMany(b => b.MemberStateHistories).HasForeignKey(c => c.MemberStateId); // FK_MemberStatusHistory_MemberStatus
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>