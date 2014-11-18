namespace DigitalLibrary.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Web.Mvc;

    using AutoMapper;

    using DigitalLibrary.Data;
    using DigitalLibrary.Web.Areas.Administration.Controllers.Base;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using EditModel = DigitalLibrary.Web.Areas.Administration.ViewModels.Comments.CommentEditModel;
    using Model = DigitalLibrary.Models.Comment;
    using ViewModel = DigitalLibrary.Web.Areas.Administration.ViewModels.Comments.CommentViewModel;

    public class CommentController : KendoGridCRUDController
    {
        public CommentController(IDigitalLibraryData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return this.View();
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
            var comment = this.Data.Comments.GetById(model.Id);
            this.Data.Comments.Delete(comment);
            this.Data.SaveChanges();
            return this.GridOperation(model, request);
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Comments.All().Select(ViewModel.FromComment);
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Comments.GetById(id) as T;
        }
    }
}