namespace DigitalLibrary.Web.Areas.Administration.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Collections;

    using Microsoft.AspNet.Identity;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using DigitalLibrary.Data;
    using DigitalLibrary.Web.Areas.Administration.Controllers.Base;

    using Model = DigitalLibrary.Models.User;
   // using ViewModel = DigitalLibrary.Web.Areas.Administration.ViewModels.Genres.GenreViewModel;
    using DigitalLibrary.Web.Infrastructure.Mapping;
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Users;

    public class UserController : KendoGridAdministrationController
    {
        public UserController(IDigitalLibraryData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Users.All().Select(UserViewModel.FromUser);
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Users.GetById(id) as T;
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, Model model)
        {
            base.Destroy<Model>(model.Id);
            return this.GridOperation(model, request);
        }
    }
}