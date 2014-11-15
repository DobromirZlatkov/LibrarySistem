namespace DigitalLibrary.Web.Areas.Administration.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Collections;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using DigitalLibrary.Data;
    using DigitalLibrary.Web.Areas.Administration.Controllers.Base;

    using Model = DigitalLibrary.Models.Genre;
    using ViewModel = DigitalLibrary.Web.Areas.Administration.ViewModels.Genres.GenreViewModel;
    using DigitalLibrary.Web.Infrastructure.Mapping;
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Genres;

    public class GenreController : KendoGridAdministrationController
    {

        public GenreController(IDigitalLibraryData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Genres.All().Select(GenreViewModel.FromGenreAdmin);
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Genres.GetById(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Create<Model>(model);
            if (dbModel != null) model.Id = dbModel.Id;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update<Model, ViewModel>(model, model.Id);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var genre = this.Data.Genres.GetById(model.Id.Value);

            foreach (var work in genre.Works)
            {
                foreach (var comment in work.Comments)
                {
                     base.Destroy<Comment>(comment.Id);
                }
                base.Destroy<Work>(work.Id);
            }

            base.Destroy<Model>(model.Id);
            return this.GridOperation(model, request);
        }
    }
}