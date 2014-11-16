namespace DigitalLibrary.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using DigitalLibrary.Data;
    using DigitalLibrary.Web.Areas.Administration.Controllers.Base;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Users;
 
    using Model = DigitalLibrary.Models.User;

    public class UserController : KendoGridCRUDController
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