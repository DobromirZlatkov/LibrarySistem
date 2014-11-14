using DigitalLibrary.Data;
using DigitalLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalLibrary.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IDigitalLibraryData Data { get; set; }

        public BaseController(IDigitalLibraryData data)
        {
            this.Data = data;
        }


        protected IdentityManager IdentityManager { get; set; }

        protected User CurrentUser { get; set; }
    }
}