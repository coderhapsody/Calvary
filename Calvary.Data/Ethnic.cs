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
    // Ethnic
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.17.1.0")]
    public partial class Ethnic
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public string KlasisMappingValue { get; set; } // KlasisMappingValue
        public DateTime CreatedWhen { get; set; } // CreatedWhen
        public string CreatedBy { get; set; } // CreatedBy
        public DateTime ChangedWhen { get; set; } // ChangedWhen
        public string ChangedBy { get; set; } // ChangedBy

        // Reverse navigation
        public virtual ICollection<Member> Members { get; set; } // Member.FK_Member_Ethnic
        
        public Ethnic()
        {
            Members = new List<Member>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
