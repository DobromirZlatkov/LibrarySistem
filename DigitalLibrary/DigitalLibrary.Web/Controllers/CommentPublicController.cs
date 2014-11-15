namespace DigitalLibrary.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using DigitalLibrary.Data;
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.ViewModels.Comment;

    [Authorize]
    public class CommentPublicController : BaseController
    {
        public CommentPublicController(IDigitalLibraryData data)
            : base(data)
        {

        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentPublicPostModel commentModel)
        {
            if (ModelState.IsValid)
            {
                var username = this.CurrentUser.UserName;
                var userId = this.CurrentUser.Id;

                this.Data.Comments.Add(new Comment()
                {
                    PostedById = userId,
                    Content = commentModel.Content,
                    WorkId = commentModel.WorkId,
                    DatePosted = DateTime.Now
                });

                this.Data.SaveChanges();

                var viewModel = new CommentPublicViewModel { PostedBy = username, Content = commentModel.Content };

                return this.PartialView("_CommentPartial", viewModel);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }
    }
}