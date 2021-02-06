using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using Calvary.Data;
using Calvary.Data.ProcedureModels;
using Calvary.ViewModels.Dashboard;

namespace Calvary.Providers
{
    public class DashboardProvider : BaseProvider
    {
        public DashboardProvider(CalvaryContext context, IPrincipal principal) : base(context, principal)
        {
        }

        public async Task<List<ProcReportUlangTahunModel>> GetTodayBirthday()
        {
            var result = await context.ProcReportUlangTahun(DateTime.Today, DateTime.Today);
            return result;
        }

        public async Task<List<MemberSummaryByGender>> GetMemberSummaryByGender()
        {
            var query = from m in context.Members
                        where !m.DeceasedDate.HasValue && !m.ResignDate.HasValue && m.ChrismationDate.HasValue
                        group m by m.Gender into g
                        select new MemberSummaryByGender
                        {
                            Gender = g.Key == "L" ? "Pria" : "Wanita",
                            Total = g.Count()

                        };
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<MemberSummaryByAge>> GetMemberSummaryByAge()
        {
            var result = new List<MemberSummaryByAge>();

            var query = from m in context.Members
                        where !m.DeceasedDate.HasValue && !m.ResignDate.HasValue && 
                               m.ChrismationDate.HasValue && m.BirthDate.HasValue                       
                        select new 
                        {
                            m.MemberNo,
                            Age = DateTime.Today.Year - m.BirthDate.Value.Year
                        };

            result.Add(new MemberSummaryByAge() { AgeInterval = "<17", Total = await query.CountAsync(m => m.Age < 17) });
            result.Add(new MemberSummaryByAge() { AgeInterval = "17-25", Total = await query.CountAsync(m => m.Age >= 17 && m.Age <= 25) });
            result.Add(new MemberSummaryByAge() { AgeInterval = "26-35", Total = await query.CountAsync(m => m.Age >= 26 && m.Age <= 35) });
            result.Add(new MemberSummaryByAge() { AgeInterval = "36-45", Total = await query.CountAsync(m => m.Age >= 36 && m.Age <= 45) });
            result.Add(new MemberSummaryByAge() { AgeInterval = "46-55", Total = await query.CountAsync(m => m.Age >= 46 && m.Age <= 55) });
            result.Add(new MemberSummaryByAge() { AgeInterval = "56-65", Total = await query.CountAsync(m => m.Age >= 56 && m.Age <= 65) });

            return result.Where(m => m.Total > 0);

        }
    }
}
