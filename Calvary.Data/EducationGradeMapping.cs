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
    // EducationGrade
    public partial class EducationGradeMapping : EntityTypeConfiguration<EducationGrade>
    {
        public EducationGradeMapping()
            : this("dbo")
        {
        }
 
        public EducationGradeMapping(string schema)
        {
            ToTable(schema + ".EducationGrade");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.Seq).HasColumnName("Seq").IsRequired().HasColumnType("int");
            Property(x => x.CreatedWhen).HasColumnName("CreatedWhen").IsRequired().HasColumnType("datetime");
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.ChangedWhen).HasColumnName("ChangedWhen").IsRequired().HasColumnType("datetime");
            Property(x => x.ChangedBy).HasColumnName("ChangedBy").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.RowVersion).HasColumnName("RowVersion").IsRequired().IsFixedLength().HasColumnType("timestamp").HasMaxLength(8).IsRowVersion().IsConcurrencyToken().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
