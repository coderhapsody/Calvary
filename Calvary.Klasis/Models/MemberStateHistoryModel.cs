using System;

namespace Calvary.Klasis.Models
{
    internal class MemberStateHistoryModel
    {
        public int MemberId { get; set; }
        public string MemberNo { get; set; }
        public string StateCode { get; internal set; }
        public string StateDescription { get; internal set; }
        public DateTime EffectiveDate { get; internal set; }
        public string Notes { get; internal set; }

        public MemberStateHistoryModel()
        {                
        }

        public MemberStateHistoryModel(int memberId, string memberCode, string stateCode, string stateDescription, DateTime effDate, string notes) : this()
        {
            MemberId = memberId;
            MemberNo = memberCode;
            StateCode = stateCode;
            StateDescription = stateDescription;
            EffectiveDate = effDate;
            Notes = notes;
        }

        public MemberStateHistoryModel(int memberId, string memberCode, MemberMutation memberMutation, DateTime effDate, string notes) :
            this(memberId, memberCode, memberMutation.Code, memberMutation.Description, effDate, notes)
        {
        }
    }
}
