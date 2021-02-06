using System;
using System.Diagnostics;

namespace Calvary.Data
{
    [DebuggerStepThrough]    
    public static class EntityHelper
    {
        public static void SetAuditFieldsForInsert(dynamic entity, string userName)
        {
            try
            {
                entity.ChangedWhen = DateTime.Now;
                entity.ChangedBy = userName;
                entity.CreatedWhen = DateTime.Now;
                entity.CreatedBy = userName;                
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException) { }
        }

        public static void SetAuditFieldsForUpdate(dynamic entity, string userName)
        {
            try
            {
                entity.ChangedWhen = DateTime.Now;
                entity.ChangedBy = userName;
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException) { }
        }
    }
}
