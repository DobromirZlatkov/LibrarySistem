namespace DigitalLibrary.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using DigitalLibrary.Data;
    using DigitalLibrary.Web.Infrastructure.Services.Contracts;
    using DigitalLibrary.Web.ViewModels.Comment;

    [Authorize]
    public class CommentPublicController : BaseController
    {
        private ICommentService commentServices;

        public CommentPublicController(IDigitalLibraryData data, ICommentService commentServices)
            : base(data)
        {
            this.commentServices = commentServices;
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentPublicPostModel commentModel)
        {
            if (ModelState.IsValid)
            {
                var viewModel = this.commentServices.Create(commentModel, this.CurrentUser);

                return this.PartialView("_CommentPartial", viewModel);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }

        public ActionResult PostCommentView(int workId)
        {
            return this.PartialView("_PostCommentTemplate", workId);
        }
    }
}