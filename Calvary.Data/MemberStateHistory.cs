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
    public partial class MemberStateHistory
    {
        public int Id { get; set; } // Id (Primary key)
        public int MemberId { get; set; } // MemberId
        public int MemberStateId { get; set; } // MemberStateId
        public DateTime EffDate { get; set; } // EffDate
        public string Notes { get; set; } // Notes
        public DateTime CreatedWhen { get; set; } // CreatedWhen
        public string CreatedBy { get; set; } // CreatedBy
        public DateTime ChangedWhen { get; set; } // ChangedWhen
        public string ChangedBy { get; set; } // ChangedBy
        public byte[] RowVersion { get; set; } // RowVersion

        // Foreign keys
        public virtual Member Member { get; set; } // FK_MemberStatusHistory_Member
        public virtual MemberState MemberState { get; set; } // FK_MemberStatusHistory_MemberStatus
        
        public MemberStateHistory()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
