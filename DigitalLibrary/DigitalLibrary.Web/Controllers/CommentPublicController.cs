namespace DigitalLibrary.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using System.Collections;

    using Microsoft.AspNet.Identity;

    using DigitalLibrary.Data;
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.ViewModels.Comment;
    using DigitalLibrary.Web.Infrastructure.Services.Contracts;


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
                var viewModel = commentServices.Create(commentModel, this.CurrentUser);

                return this.PartialView("_CommentPartial", viewModel);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }

        public ActionResult PostCommentView(int workId)
        {
            return PartialView("_PostCommentTemplate", workId);
        }
    }
}