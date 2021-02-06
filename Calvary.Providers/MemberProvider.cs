using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Calvary.Data;
using Calvary.ViewModels.Browse;
using Calvary.ViewModels.Master.Member;
using Calvary.ViewModels.Master.MemberState;
using Calvary.ViewModels.Activities.MemberVisit;

namespace Calvary.Providers
{
	public class MemberProvider : BaseProvider
	{
		public MemberProvider(CalvaryContext context, IPrincipal principal)
            : base(context, principal)
        {			
        }

        #region Member
        public void AddMember(Member member)
        {
            context.Members.Add(member);
            SetAuditFields(member);

            context.SaveChanges();
        }		
		
		public void UpdateMember(Member member)
        {
            SetAuditFields(member);
            context.SaveChanges();
        }	

		public void DeleteMember(int memberId)
        {
            var selectedMember = context.Members.SingleOrDefault(member => member.Id == memberId);
            if(selectedMember != null)
            {
                var queryMemberStatusHistory = context.MemberStateHistories.Where(x => x.MemberId == memberId);
                foreach (var memberStatusHistory in queryMemberStatusHistory)
                    context.MemberStateHistories.Remove(memberStatusHistory);
                context.Members.Remove(selectedMember);
                context.SaveChanges();
            }
        }

		public IQueryable<ListMemberViewModel> List(bool includeResign = true, bool includeDeceased = true)
        {
            IQueryable<Member> queryMember = context.Members;

            if (!includeResign)
                queryMember = queryMember.Where(m => !m.ResignDate.HasValue);

            if (!includeDeceased)
                queryMember = queryMember.Where(m => !m.DeceasedDate.HasValue);

            var query = from member in queryMember
                        select new ListMemberViewModel
                        {
                            Id = member.Id,
							Name = member.Name,
                            BirthDate = member.BirthDate,
                            CellPhone1 = member.CellPhone1,
                            Email = member.Email,
                            Gender = member.Gender == "L" ? "Laki-Laki" : "Perempuan",
                            HomePhone = member.HomePhone,
                            MemberNo = member.MemberNo,
                            NickName = member.NickName,
                            ChangedWhen = member.ChangedWhen
                        };

            return query;
        }

        public bool IsMemberNoValid(int memberId, string memberNo)
        {
            IQueryable<Member> query = context.Members;

            if (memberId == 0)
                query = query.Where(m => m.MemberNo == memberNo);
            else
                query = query.Where(m => m.Id != memberId && m.MemberNo == memberNo);

            return !query.Any();
        }

        public IEnumerable<Member> GetMembers()
        {
            var query = context.Members;
            return query.ToList();
        }

        public Member GetMember(int memberId)
        {
            var selectedMember = context.Members.SingleOrDefault(member => member.Id == memberId);
            return selectedMember;
        }

        public IQueryable<BrowseMemberViewModel> ListBrowse()
	    {
            var query = from member in context.Members
                        select new BrowseMemberViewModel
                        {
                            MemberId = member.Id,                            
                            MemberName = member.Name,
                            MemberNo = member.MemberNo,
                        };
            return query;
	    }

        public IEnumerable<BaptizedReason> GetBaptizedReasons()
        {
            context.Configuration.ProxyCreationEnabled = false;
            return context.BaptizedReasons.ToList();
        }
        #endregion

        #region Member Marriage
        public void AddMarriage(MemberMarriage memberMarriage)
        {
            context.MemberMarriages.Add(memberMarriage);
            EntityHelper.SetAuditFieldsForInsert(memberMarriage, principal.Identity.Name);
            context.SaveChanges();
        }

        public void UpdateMarriage(MemberMarriage memberMarriage)
        {
            EntityHelper.SetAuditFieldsForUpdate(memberMarriage, principal.Identity.Name);
            context.Entry(memberMarriage).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteMarriage(int memberMarriageId)
        {
            var memberMarriage = context.MemberMarriages.SingleOrDefault(m => m.Id == memberMarriageId);
            if(memberMarriage != null)
            {
                context.MemberMarriages.Remove(memberMarriage);
                context.SaveChanges();
                
            }
        }


        public IEnumerable<MemberMarriageViewModel> ListMarriage(int memberId)
        {
            var query = from marriage in context.MemberMarriages
                        join member in context.Members on marriage.SpouseId equals member.Id into g1
                        join shrine in context.Shrines on marriage.ShrineId equals shrine.Id into g2
                        from marriedMember in g1.DefaultIfEmpty()
                        from marriageShrine in g2.DefaultIfEmpty()
                        where marriage.MemberId == memberId
                        select new MemberMarriageViewModel
                        {
                            Id = marriage.Id,
                            MarriedToMemberNo = marriedMember.MemberNo,
                            Priest = marriage.Priest,
                            SpouseName = marriage.SpouseName,
                            ShrineName = marriageShrine.Name,
                            MarriageDate = marriage.MarriageDate
                        };
            return query.ToList();
        }

        public MemberMarriage GetMarriage(int memberMarriageId)
        {
            return context.MemberMarriages.SingleOrDefault(m => m.Id == memberMarriageId);
        }

        #endregion

        #region MemberState
        public void AddMemberState(Data.MemberState memberState)
        {
            context.MemberStates.Add(memberState);
            SetAuditFields(memberState);

            context.SaveChanges();
        }

        public void UpdateMemberState(Data.MemberState memberState)
        {
            SetAuditFields(memberState);
            context.SaveChanges();
        }

        public void DeleteMemberState(int memberStateId)
        {
            var selectedMemberState = context.MemberStates.SingleOrDefault(job => job.Id == memberStateId);
            if (selectedMemberState != null)
            {
                context.MemberStates.Remove(selectedMemberState);
                context.SaveChanges();
            }
        }

        public IQueryable<ListMemberStateViewModel> ListMemberState()
        {
            var query = from memberState in context.MemberStates                                                
                        select new ListMemberStateViewModel
                        {
                            Id = memberState.Id,
                            Code = memberState.Code,
                            Description = memberState.Description,
                            IsActive = memberState.IsActive
                        };
            return query;
        }

        public IEnumerable<Data.MemberState> GetMemberStates()
        {
            context.Configuration.ProxyCreationEnabled = false;
            var query = context.MemberStates;
            return query.ToList();
        }

        public IEnumerable<Data.ResignReason> GetResignReasons()
        {
            context.Configuration.ProxyCreationEnabled = false;
            var query = context.ResignReasons;
            return query.ToList();
        }

        public MemberState GetMemberState(int memberStateId)
        {
            var selectedMemberState = context.MemberStates.SingleOrDefault(memberState => memberState.Id == memberStateId);
            return selectedMemberState;
        }
        #endregion

        #region Member Visit
        public void AddMemberVisit(MemberVisit memberVisit)
        {
            context.MemberVisits.Add(memberVisit);
            SetAuditFields(memberVisit);

            context.SaveChanges();
        }

        public void UpdateMemberVisit(MemberVisit memberVisit)
        {
            SetAuditFields(memberVisit);
            context.SaveChanges();
        }

        public void DeleteMemberVisit(int memberVisitId)
        {
            var selectedMemberVisit = context.MemberVisits.SingleOrDefault(job => job.Id == memberVisitId);
            if (selectedMemberVisit != null)
            {
                context.MemberVisits.Remove(selectedMemberVisit);
                context.SaveChanges();
            }
        }


        public IQueryable<ListMemberVisitViewModel> ListMemberVisit(int memberId)
        {
            var query = from memberVisit in context.MemberVisits
                        join member in context.Members on memberVisit.MemberId equals member.Id
                        join visitResult in context.VisitResults on memberVisit.VisitResultId equals visitResult.Id
                        where memberVisit.MemberId == memberId
                        select new ListMemberVisitViewModel
                        {
                            Id = memberVisit.Id,
                            MemberNo = member.MemberNo,
                            MemberName = member.Name,            
                            VisitDate = memberVisit.VisitDate,
                            VisitResultId = visitResult.Id,
                            VisitResultName = visitResult.Name,
                            Notes = memberVisit.Notes                        
                        };
            return query;
        }

        public void AddMemberStateHistory(MemberStateHistory memberStateHist)
        {
            context.Entry(memberStateHist).State = System.Data.Entity.EntityState.Added;
            SetAuditFields(memberStateHist);
            context.SaveChanges();
        }


        public void UpdateMemberStateHistory(MemberStateHistory memberStateHist)
        {
            context.Entry(memberStateHist).State = System.Data.Entity.EntityState.Modified;
            SetAuditFields(memberStateHist);
            context.SaveChanges();
        }

        public MemberVisit GetMemberVisit(int memberVisitId)
        {
            var selectedMemberVisit = context.MemberVisits.SingleOrDefault(memberState => memberState.Id == memberVisitId);
            return selectedMemberVisit;
        }
        #endregion

        #region MemberStateHistory

        public MemberStateHistory GetMemberStateHistory(int memberStateHistoryId)
        {
            return context.MemberStateHistories.SingleOrDefault(m => m.Id == memberStateHistoryId);
        }


        public IQueryable<ListMemberStateHistoryViewModel> ListMemberStateHistory(int memberId)
        {
            var query = from memberStateHist in context.MemberStateHistories
                        join member in context.Members on memberStateHist.MemberId equals member.Id
                        join memberState in context.MemberStates on memberStateHist.MemberStateId equals memberState.Id
                        where member.Id == memberId
                        select new ListMemberStateHistoryViewModel
                        {
                            Id = memberStateHist.Id,
                            MemberId = member.Id,
                            MemberNo = member.MemberNo,
                            MemberName = member.Name,
                            MemberStateId = memberState.Id,
                            MemberStateDescription = memberState.Description,
                            Notes = memberStateHist.Notes,
                            EffDate = memberStateHist.EffDate
                        };
            return query;            
        }

        public void DeleteMemberStateHistory(int memberStateHistoryId)
        {
            var selectedMemberStateHistory = context.MemberStateHistories.SingleOrDefault(job => job.Id == memberStateHistoryId);
            if (selectedMemberStateHistory != null)
            {
                context.MemberStateHistories.Remove(selectedMemberStateHistory);
                context.SaveChanges();
            }
        }
        #endregion

        #region Member Notes
        public IEnumerable<MemberNote> ListNotes(int memberId)
        {
            context.Configuration.ProxyCreationEnabled = false;
            var query = context.MemberNotes.Where(m => m.MemberId == memberId);
            return query.ToList();
        }

        public void AddMemberNote(MemberNote memberNote)
        {
            context.Entry(memberNote).State = System.Data.Entity.EntityState.Added;
            SetAuditFields(memberNote);
            context.SaveChanges();
        }


        public void UpdateMemberNote(MemberNote memberNote)
        {
            context.Entry(memberNote).State = System.Data.Entity.EntityState.Modified;
            SetAuditFields(memberNote);
            context.SaveChanges();
        }

        public void DeleteMemberNote(int memberNoteId)
        {
            var selectedMemberNote = context.MemberNotes.SingleOrDefault(job => job.Id == memberNoteId);
            if (selectedMemberNote != null)
            {
                context.MemberNotes.Remove(selectedMemberNote);
                context.SaveChanges();
            }
        }

        public MemberNote GetMemberNote(int memberNoteId)
        {
            var selectedMemberNote = context.MemberNotes.SingleOrDefault(job => job.Id == memberNoteId);
            return selectedMemberNote;
        }
        #endregion
    }
}
			





