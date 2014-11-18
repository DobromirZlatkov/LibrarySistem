namespace DigitalLibrary.Web.Infrastructure.Services.Contracts
{
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.ViewModels.Comment;

    public interface ICommentService
    {
        CommentPublicViewModel Create(CommentPublicPostModel commentModel, User CurrentUser);
    }
}