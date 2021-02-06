using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Calvary.Data;
using Calvary.ViewModels.Master.MeetingType;

namespace Calvary.Providers
{
    public class MeetingTypeProvider : BaseProvider
    {
        public MeetingTypeProvider(CalvaryContext context, IPrincipal principal) : base(context, principal)
        {
        }

        public void AddMeetingType(MeetingType meetingType)
        {
            context.MeetingTypes.Add(meetingType);
            SetAuditFields(meetingType);

            context.SaveChanges();
        }

        public void UpdateMeetingType(MeetingType meetingType)
        {
            SetAuditFields(meetingType);
            context.SaveChanges();
        }

        public void DeleteMeetingType(int worshipTypeId)
        {
            var selectedMeetingType = context.MeetingTypes.SingleOrDefault(worshipType => worshipType.Id == worshipTypeId);
            if (selectedMeetingType != null)
            {
                context.MeetingTypes.Remove(selectedMeetingType);
                context.SaveChanges();
            }
        }

        public IQueryable<ListMeetingTypeViewModel> List()
        {
            var query = from meetingType in context.MeetingTypes
                        select new ListMeetingTypeViewModel
                        {
                            Id = meetingType.Id,
                            Name = meetingType.Name,
                            IsActive = meetingType.IsActive
                        };
            return query;
        }

        public IEnumerable<MeetingType> GetMeetingTypes()
        {
            context.Configuration.ProxyCreationEnabled = false;
            var query = context.MeetingTypes;
            return query.ToList();
        }

        public MeetingType GetMeetingType(int meetingTypeId)
        {
            var selectedMeetingType = context.MeetingTypes.SingleOrDefault(meetingType => meetingType.Id == meetingTypeId);
            return selectedMeetingType;
        }
    }
}
