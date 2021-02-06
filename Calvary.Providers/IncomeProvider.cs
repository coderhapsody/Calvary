using Calvary.Data;
using Calvary.ViewModels.Activities.Worship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Calvary.Providers
{
    public class IncomeProvider : BaseProvider
    {
        public IncomeProvider(CalvaryContext context, IPrincipal principal)
            : base(context, principal)
        {
        }

        #region Income (Persembahan)
        public void AddIncome(Income income)
        {
            context.Incomes.Add(income);
            SetAuditFields(income);
            context.SaveChanges();
        }

        public void UpdateIncome(Income income)
        {
            SetAuditFields(income);
            context.SaveChanges();
        }

        public void DeleteIncome(int incomeId)
        {
            var worshipIncome = context.Incomes.SingleOrDefault(w => w.Id == incomeId);
            if (worshipIncome != null)
            {
                context.Incomes.Remove(worshipIncome);
                context.SaveChanges();
            }
        }

        public Income GetIncome(int incomeId)
        {
            context.Configuration.ProxyCreationEnabled = false;
            return context.Incomes.SingleOrDefault(w => w.Id == incomeId);
        }

        public IQueryable<ListIncomeViewModel> GetIncomes(int month, int year)
        {
            context.Configuration.ProxyCreationEnabled = false;
            var query = from income in context.Incomes
                        join incomeType in context.IncomeTypes on income.IncomeTypeId equals incomeType.Id
                        join worship in context.Worships on income.WorshipId equals worship.Id into worshipLeftJoin
                        from worshipEx in worshipLeftJoin.DefaultIfEmpty()
                        where income.ReceivedDate.Month == month
                           && income.ReceivedDate.Year == year
                        select new ListIncomeViewModel
                        {
                            IncomeTypeName = incomeType.Name,
                            Id = income.Id,
                            WorshipName = worshipEx.Subject,
                            Amount = income.Amount,
                            ReceivedBy = income.ReceivedBy,
                            ReceivedDate = income.ReceivedDate
                        };
            return query;
        }

        public IEnumerable<ListIncomeViewModel> ListByWorship(int worshipId)
        {
            var query = from income in context.Incomes.Include("IncomeTypes")
                        where income.WorshipId == worshipId
                        select new ListIncomeViewModel
                        {
                            Id = income.Id,
                            IncomeTypeName = income.IncomeType.Name,
                            Amount = income.Amount,
                            ReceivedBy = income.ReceivedBy,
                            ReceivedDate = income.ReceivedDate                            
                        };
            return query.ToList();
        }
        #endregion
    }
}
