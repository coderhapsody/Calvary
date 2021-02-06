using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Calvary.Data;
using Calvary.ViewModels.Master.Ethnic;

namespace Calvary.Providers
{
	public class EthnicProvider : BaseProvider
	{
		public EthnicProvider(CalvaryContext context, IPrincipal principal)
            : base(context, principal)
        {			
        }
		
		public void AddEthnic(Ethnic ethnic)
        {
            context.Ethnics.Add(ethnic);
            SetAuditFields(ethnic);

            context.SaveChanges();
        }		
		
		public void UpdateEthnic(Ethnic ethnic)
        {
            SetAuditFields(ethnic);
            context.SaveChanges();
        }	

		public void DeleteEthnic(int ethnicId)
        {
            var selectedEthnic = context.Ethnics.SingleOrDefault(ethnic => ethnic.Id == ethnicId);
            if(selectedEthnic != null)
            {
                context.Ethnics.Remove(selectedEthnic);
                context.SaveChanges();
            }
        }

		public IQueryable<ListEthnicViewModel> List()
        {
            var query = from ethnic in context.Ethnics
                        select new ListEthnicViewModel
                        {
                            Id = ethnic.Id,
							Name = ethnic.Name,
                            KlasisMappingValue = ethnic.KlasisMappingValue
                        };
            return query;
        }

        public IEnumerable<Ethnic> GetEthnics()
        {
            context.Configuration.ProxyCreationEnabled = false;
            var query = context.Ethnics.OrderBy(ethnic => ethnic.Name);
            return query.ToList();
        }

        public Ethnic GetEthnic(int ethnicId)
        {
            var selectedEthnic = context.Ethnics.SingleOrDefault(ethnic => ethnic.Id == ethnicId);
            return selectedEthnic;
        }
	}
}
			