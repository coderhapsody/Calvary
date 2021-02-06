using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Calvary.Data;
using Calvary.ViewModels.Activities.ScheduleGroup;
using Calvary.ViewModels.Activities.Schedule;

namespace Calvary.Providers
{
    public class ScheduleProvider : BaseProvider
    {
        public ScheduleProvider(CalvaryContext context, IPrincipal principal) 
            : base(context, principal)
        {
        }

        #region ScheduleGroup
        public void AddScheduleGroup(ScheduleGroup scheduleGroup)
        {
            context.ScheduleGroups.Add(scheduleGroup);
            SetAuditFields(scheduleGroup);

            context.SaveChanges();
        }

        public void UpdateScheduleGroup(ScheduleGroup scheduleGroup)
        {
            SetAuditFields(scheduleGroup);
            context.Entry(scheduleGroup).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteScheduleGroup(int scheduleGroupId)
        {
            var scheduleGroup = context.ScheduleGroups.SingleOrDefault(sch => sch.Id == scheduleGroupId);
            if (scheduleGroup != null)
            {
                context.ScheduleGroups.Remove(scheduleGroup);
                context.SaveChanges();
            }
        }

        public IQueryable<ListScheduleGroupViewModel> ListScheduleGroups()
        {
            var query = from scheduleGroup in context.ScheduleGroups
                        select new ListScheduleGroupViewModel
                        {
                            Id = scheduleGroup.Id,
                            Name = scheduleGroup.Name,
                            IsActive = scheduleGroup.IsActive
                        };
            return query;
        }

        public IEnumerable<ScheduleGroup> GetScheduleGroups(bool activeOnly = false)
        {
            context.Configuration.ProxyCreationEnabled = false;
            IQueryable<ScheduleGroup> query = context.ScheduleGroups;

            if (activeOnly)
                query = query.Where(sch => sch.IsActive);

            return query.ToList();
        }

        public ScheduleGroup GetScheduleGroup(int scheduleGroupId)
        {
            var selectedScheduleGroup = context.ScheduleGroups
                .SingleOrDefault(scheduleGroup => scheduleGroup.Id == scheduleGroupId);
            return selectedScheduleGroup;
        }
        #endregion


        #region Schedule
        public void AddSchedule(Schedule schedule)
        {
            context.Schedules.Add(schedule);
            SetAuditFields(schedule);

            context.SaveChanges();
        }

        public void UpdateSchedule(Schedule schedule)
        {
            SetAuditFields(schedule);
            context.Entry(schedule).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteSchedule(int scheduleId)
        {
            var schedule = context.Schedules.SingleOrDefault(sch => sch.Id == scheduleId);
            if (schedule != null)
            {
                context.Schedules.Remove(schedule);
                context.SaveChanges();
            }
        }

        public IQueryable<ListScheduleViewModel> ListSchedules()
        {
            var query = from schedule in context.Schedules
                        join scheduleGroup in context.ScheduleGroups on schedule.ScheduleGroupId equals scheduleGroup.Id
                        select new ListScheduleViewModel
                        {
                            Id = schedule.Id,
                            Title = schedule.Title,
                            DateFrom = schedule.DateFrom,
                            DateTo = schedule.DateTo,
                            Notes = schedule.Notes,
                            ScheduleGroupId = schedule.ScheduleGroupId,
                            ScheduleGroupName = scheduleGroup.Name,
                            IsActive = schedule.IsActive
                        };
            return query;
        }

        public IEnumerable<Schedule> GetSchedule(bool activeOnly = false)
        {
            context.Configuration.ProxyCreationEnabled = false;
            IQueryable<Schedule> query = context.Schedules;

            if (activeOnly)
                query = query.Where(sch => sch.IsActive);

            return query.ToList();
        }

        public Schedule GetSchedule(int scheduleId)
        {
            var selectedSchedule = context.Schedules
                .SingleOrDefault(schedule => schedule.Id == scheduleId);
            return selectedSchedule;
        }
        #endregion
    }
}
