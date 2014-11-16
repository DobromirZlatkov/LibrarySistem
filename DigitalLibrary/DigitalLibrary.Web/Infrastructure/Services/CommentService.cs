namespace DigitalLibrary.Web.Infrastructure.Services
{
    using System;

    using DigitalLibrary.Data;
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Controllers;
    using DigitalLibrary.Web.Infrastructure.Services.Contracts;
    using DigitalLibrary.Web.ViewModels.Comment;

    public class CommentService : BaseController, ICommentService
    {
        public CommentService(IDigitalLibraryData data)
            : base(data)
        {
        }

        public CommentPublicViewModel Create(CommentPublicPostModel commentModel, User CurrentUser)
        {
           var test = CurrentUser;
           var username = CurrentUser.UserName;
           var userId = CurrentUser.Id;

            this.Data.Comments.Add(new Comment()
            {
                PostedById = userId,
                Content = commentModel.Content,
                WorkId = commentModel.WorkId,
                DatePosted = DateTime.Now
            });

            this.Data.SaveChanges();

            var viewModel = new CommentPublicViewModel { PostedBy = username, Content = commentModel.Content };

            return viewModel;
        }
    }
}