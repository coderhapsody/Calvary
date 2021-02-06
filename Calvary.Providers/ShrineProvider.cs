using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using Calvary.Data;
using Calvary.ViewModels.Master.Shrine;


namespace Calvary.Providers
{
	public class ShrineProvider : BaseProvider
	{
		public ShrineProvider(CalvaryContext context, IPrincipal principal)
            : base(context, principal)
        {			
        }
		
		public void AddShrine(Shrine shrine)
        {
		    context.Shrines.Add(shrine);
            SetAuditFields(shrine);

            context.SaveChanges();
        }		
		
		public void UpdateShrine(Shrine shrine)
        {
            SetAuditFields(shrine);
            context.SaveChanges();
        }	

		public void DeleteShrine(int shrineId)
        {
            var selectedShrine = context.Shrines.SingleOrDefault(shrine => shrine.Id == shrineId);
            if(selectedShrine != null)
            {
                context.Shrines.Remove(selectedShrine);
                context.SaveChanges();
            }
        }

		public IQueryable<ListShrineViewModel> List()
        {
            var query = from shrine in context.Shrines
                        select new ListShrineViewModel
                        {
                            Id = shrine.Id,
							Name = shrine.Name,
                            Address = shrine.Address,
                            Phone = shrine.Phone
                        };
            return query;
        }


        public IQueryable<ListShrineViewModel> ListBrowse()
        {
            var query = from shrine in context.Shrines
                        select new ListShrineViewModel
                        {
                            Id = shrine.Id,
                            Name = shrine.Name,
                            Address = shrine.Address,
                            Phone = shrine.Phone
                        };
            return query;
        }

        public IEnumerable<Shrine> GetShrines()
        {
            context.Configuration.ProxyCreationEnabled = false;
            var query = context.Shrines.OrderBy(shrine => shrine.Name);
            return query.ToList();
        }

        public Shrine GetShrine(int shrineId)
        {
            var selectedShrine = context.Shrines.SingleOrDefault(shrine => shrine.Id == shrineId);            
            return selectedShrine;
        }
	}
}
			





