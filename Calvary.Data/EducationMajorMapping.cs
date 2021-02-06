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
    // EducationMajor
    public partial class EducationMajorMapping : EntityTypeConfiguration<EducationMajor>
    {
        public EducationMajorMapping()
            : this("dbo")
        {
        }
 
        public EducationMajorMapping(string schema)
        {
            ToTable(schema + ".EducationMajor");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName("Name").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.CreatedWhen).HasColumnName("CreatedWhen").IsRequired().HasColumnType("datetime");
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.ChangedWhen).HasColumnName("ChangedWhen").IsRequired().HasColumnType("datetime");
            Property(x => x.ChangedBy).HasColumnName("ChangedBy").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
