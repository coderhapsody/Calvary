using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Calvary.Data;
using Calvary.ViewModels.Master.EducationMajor;

namespace Calvary.Providers
{
	public class EducationMajorProvider : BaseProvider
	{
		public EducationMajorProvider(CalvaryContext context, IPrincipal principal)
            : base(context, principal)
        {			
        }
		
		public void AddEducationMajor(EducationMajor educationMajor)
        {
            context.EducationMajors.Add(educationMajor);
            SetAuditFields(educationMajor);

            context.SaveChanges();
        }		
		
		public void UpdateEducationMajor(EducationMajor educationMajor)
        {
            SetAuditFields(educationMajor);
            context.SaveChanges();
        }	

		public void DeleteEducationMajor(int educationMajorId)
		{
		    var selectedEducationMajor =
		        context.EducationMajors.SingleOrDefault(educationMajor => educationMajor.Id == educationMajorId);
            if(selectedEducationMajor != null)
            {
                context.EducationMajors.Remove(selectedEducationMajor);
                context.SaveChanges();
            }
        }

		public IQueryable<ListEducationMajorViewModel> List()
        {
            var query = from educationMajor in context.EducationMajors
                        select new ListEducationMajorViewModel
                        {
                            Id = educationMajor.Id,
							Name = educationMajor.Name
                        };
            return query;
        }

        public IEnumerable<EducationMajor> GetEducationMajors()
        {
            context.Configuration.ProxyCreationEnabled = false;
            var educationMajors = context.EducationMajors.OrderBy(ed => ed.Name);
            return educationMajors.ToList();
        }

        public EducationMajor GetEducationMajor(int educationMajorId)
        {
            var selectedEducationMajor = context.EducationMajors
                .SingleOrDefault(educationMajor => educationMajor.Id == educationMajorId);
            return selectedEducationMajor;
        }
	}
}
			