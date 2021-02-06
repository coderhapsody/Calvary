using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Calvary.Data;
using Calvary.ViewModels.Master.EducationGrade;

namespace Calvary.Providers
{
	public class EducationGradeProvider : BaseProvider
	{
		public EducationGradeProvider(CalvaryContext context, IPrincipal principal)
            : base(context, principal)
        {			
        }
		
		public void AddEducationGrade(EducationGrade educationGrade)
        {
            context.EducationGrades.Add(educationGrade);
            SetAuditFields(educationGrade);

            context.SaveChanges();
        }		
		
		public void UpdateEducationGrade(EducationGrade educationGrade)
        {            
            SetAuditFields(educationGrade);
            context.Entry(educationGrade).State = System.Data.Entity.EntityState.Modified;            
            context.SaveChanges();
        }	

		public void DeleteEducationGrade(int educationGradeId)
        {
            var selectedEducationGrade = context.EducationGrades.SingleOrDefault(educationGrade => educationGrade.Id == educationGradeId);
            if(selectedEducationGrade != null)
            {
                context.EducationGrades.Remove(selectedEducationGrade);
                context.SaveChanges();
            }
        }

		public IQueryable<ListEducationGradeViewModel> List()
        {
            var query = from educationGrade in context.EducationGrades
                        select new ListEducationGradeViewModel
                        {
                            Id = educationGrade.Id,
                            Name = educationGrade.Name,
                            Seq = educationGrade.Seq		
                        };
            return query;
        }

        public IEnumerable<EducationGrade> GetEducationGrades()
        {
            context.Configuration.ProxyCreationEnabled = false;
            var query = context.EducationGrades.OrderBy(o => o.Seq);
            return query.ToList();
        }

        public EducationGrade GetEducationGrade(int educationGradeId)
        {
            var selectedEducationGrade = context.EducationGrades
                .SingleOrDefault(educationGrade => educationGrade.Id == educationGradeId);
            return selectedEducationGrade;
        }
	}
}
			