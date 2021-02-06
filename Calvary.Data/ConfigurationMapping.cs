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
    // Configuration
    public partial class ConfigurationMapping : EntityTypeConfiguration<Configuration>
    {
        public ConfigurationMapping()
            : this("dbo")
        {
        }
 
        public ConfigurationMapping(string schema)
        {
            ToTable(schema + ".Configuration");
            HasKey(x => x.Key);

            Property(x => x.Key).HasColumnName("Key").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Value).HasColumnName("Value").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(100);
            Property(x => x.ChangedWhen).HasColumnName("ChangedWhen").IsRequired().HasColumnType("datetime");
            Property(x => x.ChangedWho).HasColumnName("ChangedWho").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
// </auto-generated>