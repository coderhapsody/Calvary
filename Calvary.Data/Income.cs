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
    public partial class Income
    {
        public int Id { get; set; } // Id (Primary key)
        public int IncomeTypeId { get; set; } // IncomeTypeId
        public int? WorshipId { get; set; } // WorshipId
        public DateTime ReceivedDate { get; set; } // ReceivedDate
        public string ReceivedBy { get; set; } // ReceivedBy
        public string Currency { get; set; } // Currency
        public decimal Amount { get; set; } // Amount
        public string Notes { get; set; } // Notes
        public DateTime CreatedWhen { get; set; } // CreatedWhen
        public string CreatedBy { get; set; } // CreatedBy
        public DateTime ChangedWhen { get; set; } // ChangedWhen
        public string ChangedBy { get; set; } // ChangedBy

        // Foreign keys
        public virtual IncomeType IncomeType { get; set; } // FK_IncomeTypeId
        public virtual Worship Worship { get; set; } // FK_Income_Worship
        
        public Income()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>