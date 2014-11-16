namespace DigitalLibrary.Web.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Microsoft.AspNet.Identity;

    using DigitalLibrary.Data;
    using DigitalLibrary.Models;

   // [HandleError]
    public abstract class BaseController : Controller
    {
        protected IDigitalLibraryData Data { get; set; }

        protected IdentityManager IdentityManager { get; set; }

        protected User CurrentUser { get; private set; }

        protected User test
        {
            get
            {
                return this.Data.Users.GetById(User.Identity.GetUserId());
            }
        }

        public BaseController(IDigitalLibraryData data)
        {
            this.Data = data;
            this.IdentityManager = new IdentityManager();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.CurrentUser = this.Data.Users.All().Where(u => u.UserName == requestContext.HttpContext.User.Identity.Name).FirstOrDefault();
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}