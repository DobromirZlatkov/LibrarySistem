namespace DigitalLibrary.Web.Infrastructure.Services.Contracts
{
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.ViewModels.Like;

    public interface ILikeService
    {
         Work ExtecuteLikeOrDislikeAction(LikeSubmitModel like, User currentUser);
    }
}