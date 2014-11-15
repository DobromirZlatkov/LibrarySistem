namespace DigitalLibrary.Web.Controllers
{
    using System.Collections;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using DigitalLibrary.Data;
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Areas.Administration.Controllers.Base;
    using DigitalLibrary.Web.ViewModels.Authors;

    [Authorize]
    public class AuthorController : KendoGridAdministrationController
    {
        public AuthorController(IDigitalLibraryData data)
            : base(data)
        {
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AuthorPublicCreateModel model)
        {
            var dbModel = base.Create<Author>(model);
            if (dbModel != null) model.Id = dbModel.Id;
            ViewBag.Author = this.GetData();
            //cannot handle to refresh author dropdown
            return new HttpStatusCodeResult(HttpStatusCode.Accepted, ModelState.Values.First().ToString());
        }

        protected override IEnumerable GetData()
        {
            var allAuthors = this.Data.Authors
             .All()
             .Select(AuthorPublicListViewModel.FromAuthor);

            return allAuthors;
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Authors.GetById(id) as T;
        }
    }
}