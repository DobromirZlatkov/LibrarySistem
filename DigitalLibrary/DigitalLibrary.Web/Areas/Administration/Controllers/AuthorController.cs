namespace DigitalLibrary.Web.Areas.Administration.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Collections;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using DigitalLibrary.Data;
    using DigitalLibrary.Web.Areas.Administration.Controllers.Base;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Authors;
    using DigitalLibrary.Web.Infrastructure.Mapping;
    using DigitalLibrary.Models;

    using Model = DigitalLibrary.Models.Author;
    using ViewModel = DigitalLibrary.Web.Areas.Administration.ViewModels.Authors.AuthorViewModel;
    using System.Linq;
    using AutoMapper;


    public class AuthorController : KendoGridAdministrationController
    {
        public AuthorController(IDigitalLibraryData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Authors.All().Select(AuthorViewModel.FromAuthor);
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Authors.GetById(id) as T;
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
            if (model != null)
            {
                var author = this.Data.Authors.GetById(model.Id.Value);
                foreach (var work in author.Works)
                {
                    foreach (var comment in work.Comments)
                    {
                        this.Data.Comments.Delete(comment.Id);
                    }

                    this.Data.Works.Delete(work.Id);
                }

                this.Data.Authors.Delete(model.Id.Value);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}