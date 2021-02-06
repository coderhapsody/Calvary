using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Calvary.Data;

namespace Calvary.Providers
{
    public class KlasisProvider : BaseProvider
    {
        public KlasisProvider(CalvaryContext context, IPrincipal principal) 
            : base(context, principal)
        {
        }


        public IEnumerable<string> GetMappingValues(string mappingName)
        {
            var query = context.KlasisMappings
                .Where(map => map.MappingName == mappingName)
                .Select(map => map.MappingValue);

            return query.ToList();
        }
    }
}
