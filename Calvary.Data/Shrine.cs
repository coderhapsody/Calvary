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
    // Shrine
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.17.1.0")]
    public partial class Shrine
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name
        public string Address { get; set; } // Address
        public string City { get; set; } // City
        public string Phone { get; set; } // Phone
        public DateTime CreatedWhen { get; set; } // CreatedWhen
        public string CreatedBy { get; set; } // CreatedBy
        public DateTime ChangedWhen { get; set; } // ChangedWhen
        public string ChangedBy { get; set; } // ChangedBy

        // Reverse navigation
        public virtual ICollection<Member> Members_ChildhoodBaptizedShrineId { get; set; } // Member.FK_Member_ChildhoodBaptizedShrine
        public virtual ICollection<Member> Members_ChrismationShrineId { get; set; } // Member.FK_Member_ChrismationShrine
        public virtual ICollection<Member> Members_JoinFromShrineId { get; set; } // Member.FK_Member_JoinFromShrine
        public virtual ICollection<Member> Members_ResignToShrineId { get; set; } // Member.FK_Member_ResignToShrine
        public virtual ICollection<MemberMarriage> MemberMarriages { get; set; } // MemberMarriage.FK_MemberMarriage_Shrine
        
        public Shrine()
        {
            Members_ChildhoodBaptizedShrineId = new List<Member>();
            Members_ChrismationShrineId = new List<Member>();
            Members_JoinFromShrineId = new List<Member>();
            Members_ResignToShrineId = new List<Member>();
            MemberMarriages = new List<MemberMarriage>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
