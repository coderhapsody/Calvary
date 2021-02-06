using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Calvary.Data;
using Calvary.ViewModels.Master.WorshipType;

namespace Calvary.Providers
{
    public class WorshipTypeProvider : BaseProvider
    {
        public WorshipTypeProvider(CalvaryContext context, IPrincipal principal)
            : base(context, principal)
        {
        }
        public void AddWorshipType(WorshipType worshipType)
        {
            context.WorshipTypes.Add(worshipType);
            SetAuditFields(worshipType);

            context.SaveChanges();
        }

        public void UpdateWorshipType(WorshipType worshipType)
        {
            SetAuditFields(worshipType);
            context.SaveChanges();
        }

        public void DeleteWorshipType(int worshipTypeId)
        {
            var selectedWorshipType = context.WorshipTypes.SingleOrDefault(worshipType => worshipType.Id == worshipTypeId);
            if (selectedWorshipType != null)
            {
                context.WorshipTypes.Remove(selectedWorshipType);
                context.SaveChanges();
            }
        }

        public IQueryable<ListWorshipTypeViewModel> List()
        {
            var query = from worshipType in context.WorshipTypes
                        select new ListWorshipTypeViewModel
                        {
                            Id = worshipType.Id,
                            Name = worshipType.Name,
                            FlagUmum = worshipType.FlagUmum,
                            IsActive = worshipType.IsActive
                        };
            return query;
        }

        public IEnumerable<WorshipType> GetWorshipTypes()
        {
            context.Configuration.ProxyCreationEnabled = false;
            var query = context.WorshipTypes;
            return query.ToList();
        }

        public WorshipType GetWorshipType(int worshipTypeId)
        {
            var selectedWorshipType = context.WorshipTypes.SingleOrDefault(worshipType => worshipType.Id == worshipTypeId);
            return selectedWorshipType;
        }
    }
}
