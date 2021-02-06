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
    // KlasisMapping
    public partial class KlasisMappingMapping : EntityTypeConfiguration<KlasisMapping>
    {
        public KlasisMappingMapping()
            : this("dbo")
        {
        }
 
        public KlasisMappingMapping(string schema)
        {
            ToTable(schema + ".KlasisMapping");
            HasKey(x => new { x.MappingName, x.MappingValue });

            Property(x => x.MappingName).HasColumnName("MappingName").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.MappingValue).HasColumnName("MappingValue").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(100).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>
