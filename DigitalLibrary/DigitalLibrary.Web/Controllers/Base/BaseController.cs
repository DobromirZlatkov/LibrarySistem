using DigitalLibrary.Data;
using DigitalLibrary.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;


namespace DigitalLibrary.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IDigitalLibraryData Data { get; set; }

        public BaseController(IDigitalLibraryData data)
        {
            this.Data = data;
            this.IdentityManager = new IdentityManager();
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }


        protected IdentityManager IdentityManager { get; set; }

        protected User CurrentUser 
        {
            get { return this.Data.Users.GetById(User.Identity.GetUserId()); }
        }
    }
}