namespace DigitalLibrary.Web.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Routing;

    using DigitalLibrary.Data;
    using DigitalLibrary.Models;

    using Microsoft.AspNet.Identity;

    //[HandleError]
    public abstract class BaseController : Controller
    {
        public BaseController(IDigitalLibraryData data)
        {
            this.Data = data;
            this.IdentityManager = new IdentityManager();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        protected IDigitalLibraryData Data { get; set; }

        protected IdentityManager IdentityManager { get; set; }

        protected User CurrentUser { get; private set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.CurrentUser = this.Data.Users.All().Where(u => u.UserName == requestContext.HttpContext.User.Identity.Name).FirstOrDefault();
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}