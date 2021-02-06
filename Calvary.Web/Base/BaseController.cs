using System;
using System.Web.Mvc;
using Calvary.Providers;
using Ninject;
using Ninject.Extensions.Logging;

namespace Calvary.Web.Base
{
    public class BaseController : Controller
    {
        [Inject]
        public ILogger Logger { get; set; }

        [Inject]
        public ConfigurationProvider ConfigurationInstance { get; set; }

        [Inject]
        public SecurityProvider SecurityService { get; set; }

        public string CurrentUserName
        {
            get { return User.Identity.Name; }
        }

        protected virtual void HandleException(Exception ex)
        {
            ModelState.AddModelError(String.Empty, ex.Message);
            Logger.Error(ex.Message);
        }
    }
}