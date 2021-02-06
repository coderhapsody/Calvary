using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Calvary.Data;
using Calvary.ViewModels.Activities.Worship;

namespace Calvary.Providers
{
    public class WorshipProvider : BaseProvider
    {
        public WorshipProvider(CalvaryContext context, IPrincipal principal)
            : base(context, principal)
        {
        }

        #region Worship
        public void AddWorship(Worship worship)
        {
            context.Worships.Add(worship);
            SetAuditFields(worship);

            context.SaveChanges();
        }

        public void UpdateWorship(Worship worship)
        {
            SetAuditFields(worship);
            context.SaveChanges();
        }

        public void DeleteWorship(int worshipId)
        {
            var selectedWorship = context.Worships.SingleOrDefault(worship => worship.Id == worshipId);
            if (selectedWorship != null)
            {
                context.Worships.Remove(selectedWorship);
                context.SaveChanges();
            }
        }

        public IQueryable<ListWorshipViewModel> List()
        {
            var query = from worship in context.Worships
                        join worshipType in context.WorshipTypes on worship.WorshipTypeId equals worshipType.Id into g
                        from grouped in g.DefaultIfEmpty()
                        select new ListWorshipViewModel
                        {
                            Id = worship.Id,
                            Date = worship.Date,
                            Subject = worship.Subject,
                            Priest = worship.Priest,
                            WorshipTypeName = grouped.Name ?? String.Empty
                        };
            return query;
        }

        public IEnumerable<Worship> GetWorships()
        {
            context.Configuration.ProxyCreationEnabled = false;
            var query = context.Worships;
            return query.ToList();
        }

        public Worship GetWorship(int worshipId)
        {
            var selectedWorship = context.Worships.SingleOrDefault(worship => worship.Id == worshipId);
            return selectedWorship;
        }

        public string GetLastNotes(int currentWorshipId)
        {
            IQueryable<Worship> queryLastWorship = context.Worships;

            if (currentWorshipId > 0)
                queryLastWorship = queryLastWorship.Where(worship => worship.Id < currentWorshipId);

            queryLastWorship = queryLastWorship.OrderByDescending(worship => worship.ChangedWhen);

            Worship lastKnownWorship = queryLastWorship.FirstOrDefault();

            return lastKnownWorship != null ? lastKnownWorship.Notes : String.Empty;
        }
        #endregion


    }
}
