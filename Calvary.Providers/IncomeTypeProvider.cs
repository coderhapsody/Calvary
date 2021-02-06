using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Calvary.Data;
using Calvary.ViewModels.Master.IncomeType;

namespace Calvary.Providers
{
    public class IncomeTypeProvider : BaseProvider
    {
        public IncomeTypeProvider(CalvaryContext context, IPrincipal principal) 
            : base(context, principal)
        {
        }

        public void AddIncomeType(IncomeType incomeType)
        {
            context.IncomeTypes.Add(incomeType);
            SetAuditFields(incomeType);            
            context.SaveChanges();
        }

        public void UpdateIncomeType(IncomeType incomeType)
        {
            SetAuditFields(incomeType);
            context.SaveChanges();
        }

        public void DeleteIncomeType(int incomeTypeId)
        {
            var selectedIncomeType = context.IncomeTypes.SingleOrDefault(incomeType => incomeType.Id == incomeTypeId);
            if (selectedIncomeType != null)
            {
                context.IncomeTypes.Remove(selectedIncomeType);
                context.SaveChanges();
            }
        }

        public IQueryable<ListIncomeTypeViewModel> List()
        {
            var query = from incomeType in context.IncomeTypes
                        select new ListIncomeTypeViewModel
                        {
                            Id = incomeType.Id,
                            Name = incomeType.Name,
                            IsActive = incomeType.IsActive
                        };
            return query;
        }

        public IEnumerable<IncomeType> GetIncomeTypes()
        {
            context.Configuration.ProxyCreationEnabled = false;
            var query = context.IncomeTypes;
            return query.ToList();
        }

        public IncomeType GetIncomeType(int incomeTypeId)
        {
            var selectedIncomeType = context.IncomeTypes.SingleOrDefault(incomeType => incomeType.Id == incomeTypeId);
            return selectedIncomeType;
        }
    }
}
