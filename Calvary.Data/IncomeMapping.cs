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
    // Income
    public partial class IncomeMapping : EntityTypeConfiguration<Income>
    {
        public IncomeMapping()
            : this("dbo")
        {
        }
 
        public IncomeMapping(string schema)
        {
            ToTable(schema + ".Income");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.IncomeTypeId).HasColumnName("IncomeTypeId").IsRequired().HasColumnType("int");
            Property(x => x.WorshipId).HasColumnName("WorshipId").IsOptional().HasColumnType("int");
            Property(x => x.ReceivedDate).HasColumnName("ReceivedDate").IsRequired().HasColumnType("date");
            Property(x => x.ReceivedBy).HasColumnName("ReceivedBy").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.Currency).HasColumnName("Currency").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(5);
            Property(x => x.Amount).HasColumnName("Amount").IsRequired().HasColumnType("decimal").HasPrecision(20,2);
            Property(x => x.Notes).HasColumnName("Notes").IsOptional().IsUnicode(false).HasColumnType("text").HasMaxLength(2147483647);
            Property(x => x.CreatedWhen).HasColumnName("CreatedWhen").IsRequired().HasColumnType("datetime");
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.ChangedWhen).HasColumnName("ChangedWhen").IsRequired().HasColumnType("datetime");
            Property(x => x.ChangedBy).HasColumnName("ChangedBy").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);

            // Foreign keys
            HasOptional(a => a.Worship).WithMany(b => b.Incomes).HasForeignKey(c => c.WorshipId); // FK_Income_Worship
            HasRequired(a => a.IncomeType).WithMany(b => b.Incomes).HasForeignKey(c => c.IncomeTypeId); // FK_IncomeTypeId
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>