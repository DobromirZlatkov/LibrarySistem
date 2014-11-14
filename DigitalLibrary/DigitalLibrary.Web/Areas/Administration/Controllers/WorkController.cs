namespace DigitalLibrary.Web.Areas.Administration.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Collections;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using DigitalLibrary.Data;
    using DigitalLibrary.Web.Areas.Administration.Controllers.Base;

    using Model = DigitalLibrary.Models.Work;
    using ViewModel = DigitalLibrary.Web.Areas.Administration.ViewModels.Works.WorkViewModel;
    using DigitalLibrary.Web.Infrastructure.Mapping;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Works;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Authors;
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Genres;
    using System.Collections.Generic;


    public class WorkController : KendoGridAdministrationController
    {
        public WorkController(IDigitalLibraryData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            PopulateAuthors();
            PopulateGenres();
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Works.All().Select(ViewModel.FromWork);
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Works.GetById(id) as T;
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
            //TO DO

            return this.GridOperation(model, request);
        }


        private void PopulateAuthors()
        {
            var authors = this.Data.Authors
                        .All()
                        .Select(AuthorViewModel.FromAuthor);

            ViewData["authors"] = authors;
        }

        private void PopulateGenres()
        {
            var genres = this.Data.Genres
                        .All()
                        .Select(GenreViewModel.FromGenreAdmin);

            ViewData["genres"] = genres;
        }
    }
}