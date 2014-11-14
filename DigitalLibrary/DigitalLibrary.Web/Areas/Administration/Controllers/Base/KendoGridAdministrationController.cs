﻿namespace DigitalLibrary.Web.Areas.Administration.Controllers.Base
{
    using System.Collections;
    using System.Web.Mvc;

    using DigitalLibrary.Data;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using System.Data.Entity;
    using AutoMapper;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Base;
    using DigitalLibrary.Data.Contracts;
    using System.Collections.Generic;
    using DigitalLibrary.Models;
    using System.Linq;

    public abstract class KendoGridAdministrationController : AdminController
    {
        public KendoGridAdministrationController(IDigitalLibraryData data)
            : base(data)
        {
        }

        protected abstract IEnumerable GetData();

        protected abstract T GetById<T>(object id) where T : class;

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var ads =
              this.GetData()
              .ToDataSourceResult(request);

            return this.Json(ads);
        }

        [NonAction]
        protected virtual T Create<T>(object model) where T : class
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = Mapper.Map<T>(model);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Added);
                return dbModel;
            }

            return null;
        }

        [NonAction]
        protected virtual void Update<TModel, TViewModel>(TViewModel model, object id)
            where TModel : AuditInfo
            where TViewModel : AdministrationViewModel
        {
            if (model != null/*  && ModelState.IsValid*/)// DateTime Parse Er
            {
                var dbModel = this.GetById<TModel>(id);
                Mapper.Map<TViewModel, TModel>(model, dbModel);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);
                model.ModifiedOn = dbModel.ModifiedOn;
            }
        }

        [NonAction]
        protected virtual void Destroy<T>(object id) where T : class
        {
            //var errors = ModelState
            //    .Where(x => x.Value.Errors.Count > 0)
            //    .Select(x => new { x.Key, x.Value.Errors })
            //    .ToArray();

            if (id != null)
            {
                var dbModel = this.GetById<T>(id);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Deleted);
            }
        }

        protected JsonResult GridOperation<T>(T model, [DataSourceRequest]DataSourceRequest request)
        {
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        private void ChangeEntityStateAndSave(object dbModel, EntityState state)
        {
            var entry = this.Data.Context.Entry(dbModel);
            entry.State = state;
            this.Data.SaveChanges();
        }
    }
}