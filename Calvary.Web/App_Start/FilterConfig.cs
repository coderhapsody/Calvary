using CelloStore.FrontEnd.Attributes;
using System.Web;
using System.Web.Mvc;

namespace Calvary.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CalvaryHandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
        }
    }
}
