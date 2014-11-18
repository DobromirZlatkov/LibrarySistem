namespace DigitalLibrary.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Web.Mvc;
    using System.Linq;

    using DigitalLibrary.Data;
    using DigitalLibrary.Web.Areas.Administration.Controllers.Base;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Users;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Model = DigitalLibrary.Models.User;

    public class UserController : KendoGridCRUDController
    {
        public UserController(IDigitalLibraryData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, Model model)
        {
            var user = this.Data.Users.GetById(model.Id);

            var userWorks = this.Data.Works.All().Where(u => u.UploadedById == user.Id).Select(u => u.Id);

            foreach (var workId in userWorks)
            {
                this.Data.Works.Delete(workId);
            }

            this.Data.Users.Delete(user);
            this.Data.SaveChanges();
            return this.GridOperation(model, request);
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Users.All().Select(UserViewModel.FromUser);
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Users.GetById(id) as T;
        }
    }
}