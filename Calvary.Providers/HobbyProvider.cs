using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Calvary.Data;
using Calvary.ViewModels.Master.Hobby;

namespace Calvary.Providers
{
	public class HobbyProvider : BaseProvider
	{
		public HobbyProvider(CalvaryContext context, IPrincipal principal)
            : base(context, principal)
        {			
        }
		
		public void AddHobby(Hobby hobby)
        {
            context.Hobbies.Add(hobby);
            SetAuditFields(hobby);

            context.SaveChanges();
        }		
		
		public void UpdateHobby(Hobby hobby)
        {
            SetAuditFields(hobby);
            context.SaveChanges();
        }	

		public void DeleteHobby(int hobbyId)
        {
            var selectedHobby = context.Hobbies.SingleOrDefault(hobby => hobby.Id == hobbyId);
            if(selectedHobby != null)
            {
                context.Hobbies.Remove(selectedHobby);
                context.SaveChanges();
            }
        }

		public IQueryable<ListHobbyViewModel> List()
        {
            var query = from hobby in context.Hobbies
                        select new ListHobbyViewModel
                        {
                            Id = hobby.Id,
							Name = hobby.Name
                        };
            return query;
        }

        public IEnumerable<Hobby> GetHobbies()
        {
            context.Configuration.ProxyCreationEnabled = false;
            var query = context.Hobbies.OrderBy(hobby => hobby.Name);
            return query.ToList();
        }

        public Hobby GetHobby(int hobbyId)
        {
            var selectedHobby = context.Hobbies.SingleOrDefault(hobby => hobby.Id == hobbyId);
            return selectedHobby;
        }
	}
}
			