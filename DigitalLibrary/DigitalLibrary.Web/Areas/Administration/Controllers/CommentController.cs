namespace DigitalLibrary.Web.Areas.Administration.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Collections;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;

    using DigitalLibrary.Data;
    using DigitalLibrary.Web.Areas.Administration.Controllers.Base;

    using Model = DigitalLibrary.Models.Comment;
    using ViewModel = DigitalLibrary.Web.Areas.Administration.ViewModels.Comments.CommentViewModel;
    using EditModel = DigitalLibrary.Web.Areas.Administration.ViewModels.Comments.CommentEditModel;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Comments;
    using AutoMapper;


    public class CommentController : KendoGridCRUDController
    {
        public CommentController(IDigitalLibraryData data)
            : base(data)
        {
        }


        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Comments.All().Select(ViewModel.FromComment);
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Comments.GetById(id) as T;
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var commentEditView = new EditModel();
            Mapper.Map<ViewModel, EditModel>(model, commentEditView);
            base.Update<Model, EditModel>(commentEditView, commentEditView.Id);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Destroy<Model>(model.Id);
            return this.GridOperation(model, request);
        }
    }
}