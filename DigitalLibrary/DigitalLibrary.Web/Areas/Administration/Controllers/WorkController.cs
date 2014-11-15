﻿namespace DigitalLibrary.Web.Areas.Administration.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Collections;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using System.Linq;

    using DigitalLibrary.Data;
    using DigitalLibrary.Web.Areas.Administration.Controllers.Base;

    using Model = DigitalLibrary.Models.Work;
    using ViewModel = DigitalLibrary.Web.Areas.Administration.ViewModels.Works.WorkViewModel;
    using EditModel = DigitalLibrary.Web.Areas.Administration.ViewModels.Works.WorkEditModel;
    using DigitalLibrary.Web.Infrastructure.Mapping;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Works;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Authors;
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Genres;
    using System.Collections.Generic;
    using DigitalLibrary.Data.Logic;


    public class WorkController : KendoGridAdministrationController
    {
        public WorkController(IDigitalLibraryData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
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
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var genre = this.Data.Genres
                .All()
                .Where(g => g.GenreName == model.Genre)
                .FirstOrDefault();

            var author = this.Data.Authors
                .All()
                .Where(a => a.Name == model.Author)
                .FirstOrDefault();

            var updateModel = new EditModel()
            {
                ZipFileLink = model.ZipFileLink,
                Description = model.Description,
                GenreId = genre.Id,
                AuthorId = author.Id,
                IsApproved = model.IsApproved,
                PictureLink = model.PictureLink,
                Title = model.Title,
                Year = model.Year
            };

            base.Update<Model, EditModel>(updateModel, model.Id);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var folders = model.ZipFileLink.Split('\\').ToList();

            folders.RemoveAt(folders.Count - 1);

            base.Destroy<Model>(model.Id);

            var filePath = string.Join("/", folders);
            if (folders.Count > 1)
            {
                FileManager.DeleteFile(filePath);
            }

            return this.GridOperation(model, request);
        }
    }
}