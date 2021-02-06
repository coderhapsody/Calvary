using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Calvary.Data;
using Calvary.ViewModels.Master.Region;

namespace Calvary.Providers
{
    public class RegionProvider : BaseProvider
    {
        public RegionProvider(CalvaryContext context, IPrincipal principal)
            : base(context, principal)
        {
        }

        public void AddRegion(Region region)
        {
            if (context.Regions.Any(reg => reg.Code == region.Code && reg.Id > 0))
                throw new Exception(String.Format("Kode wilayah {0} sudah digunakan", region.Code));

            context.Regions.Remove(region);
            SetAuditFields(region);

            context.SaveChanges();
        }

        public void UpdateRegion(Region region)
        {
            SetAuditFields(region);
            context.SaveChanges();
        }

        public void DeleteRegion(int regionId)
        {
            var region = context.Regions.SingleOrDefault(reg => reg.Id == regionId);
            if(region != null)
            {
                context.Regions.Remove(region);
                context.SaveChanges();
            }
        }

        public IQueryable<ListRegionViewModel> List()
        {
            var query = from region in context.Regions
                        orderby region.ChangedWhen descending 
                        select new ListRegionViewModel
                        {
                            Id = region.Id,
                            Name = region.Name,
                            Code = region.Code,
                            IsActive = region.IsActive
                        };
            return query;
        }

        public IEnumerable<ListRegionViewModel> GetLabeledRegions(bool activeOnly = true)
        {
            var query = from region in context.Regions
                        select new ListRegionViewModel
                        {
                            Name = region.Code + " - " + region.Name,
                            Id = region.Id,
                            IsActive = region.IsActive
                        };

            if (activeOnly)
                query = query.Where(region => region.IsActive);

            return query.ToList();
        }

        public IEnumerable<Region> GetRegions(bool activeOnly = true)
        {
            IQueryable<Region> query = context.Regions;

            if(activeOnly)
                query = query.Where(region => region.IsActive);

            query = query.OrderBy(reg => reg.Code);

            return query.ToList();
        }

        public Region GetRegion(int regionId)
        {
            var region = context.Regions.SingleOrDefault(reg => reg.Id == regionId);
            return region;
        }

        public bool IsRegionCodeValid(string code, int id)
        {
            return context.Regions.Any(reg => reg.Code == code && reg.Id != id);
        }
    }
}
