namespace DigitalLibrary.Web.Controllers
{
    using System.Collections;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using DigitalLibrary.Data;
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Areas.Administration.Controllers.Base;
    using DigitalLibrary.Web.Infrastructure.Services.Contracts;
    using DigitalLibrary.Web.ViewModels.Authors;

    [Authorize]
    public class AuthorController : KendoGridCRUDController
    {
        private IAuthorService authorServices;

        public AuthorController(IDigitalLibraryData data, IAuthorService authorServices)
            : base(data)
        {
            this.authorServices = authorServices;
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthorPublicCreateModel model)
        {
            var dbModel = base.Create<Author>(model);
            if (dbModel != null) model.Id = dbModel.Id;
            return new HttpStatusCodeResult(HttpStatusCode.Accepted, ModelState.Values.First().ToString());
        }

        protected override IEnumerable GetData()
        {
            return this.authorServices.GetAuthors();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Authors.GetById(id) as T;
        }
    }
}