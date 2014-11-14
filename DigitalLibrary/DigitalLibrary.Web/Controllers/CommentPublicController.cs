using DigitalLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using DigitalLibrary.Models;
using DigitalLibrary.Web.ViewModels.Comment;
using System.Net;

namespace DigitalLibrary.Web.Controllers
{

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
                var username = this.User.Identity.GetUserName();
                var userId = this.User.Identity.GetUserId();

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