using System.Diagnostics;
using System.Security.Principal;
using Calvary.Data;

namespace Calvary.Providers
{
    public abstract class BaseProvider
    {
        protected CalvaryContext context;
        protected IPrincipal principal;
            
        protected BaseProvider(CalvaryContext context, IPrincipal principal)
        {
            this.context = context;
            this.principal = principal;            
        }

        [DebuggerStepThrough]
        protected void SetAuditFields(dynamic entity)
        {
            if (entity.Id == 0)
                EntityHelper.SetAuditFieldsForInsert(entity, principal.Identity.Name);
            else
                EntityHelper.SetAuditFieldsForUpdate(entity, principal.Identity.Name);
        }
    }
}
