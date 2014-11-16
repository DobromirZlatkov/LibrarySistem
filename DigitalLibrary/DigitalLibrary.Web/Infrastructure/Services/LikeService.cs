namespace DigitalLibrary.Web.Infrastructure.Services
{
    using System;
    using System.Linq;

    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Controllers;
    using DigitalLibrary.Web.Infrastructure.Services.Contracts;
    using DigitalLibrary.Web.ViewModels.Like;
    using DigitalLibrary.Data;

    public class LikeService : BaseController, ILikeService
    {
        public LikeService(IDigitalLibraryData data)
            : base(data)
        {
        }

        public Work ExtecuteLikeOrDislikeAction(LikeSubmitModel like, User CurrentUser)
        {
            var currentUserId = CurrentUser.Id;
            var workVotedFor = this.Data.Works.GetById(like.WorkId);
            var IfCanLike = CheckIfUserCanLikeOrDislike(like.WorkId, currentUserId, like.LikeState);

            if (like.LikeState == "like")
            {
                var yourNegativeLike = this.Data.Likes.All()
                .Where(x => x.WorkId == like.WorkId && x.LikedById == currentUserId && !x.IsPositive)
                .FirstOrDefault();
                if (yourNegativeLike != null)
                {
                    this.Data.Likes.Delete(yourNegativeLike);
                }
            }
            else
            {
                var yourPositiveLike = this.Data.Likes.All()
                .Where(x => x.WorkId == like.WorkId && x.LikedById == currentUserId && x.IsPositive)
                .FirstOrDefault();
                if (yourPositiveLike != null)
                {
                    this.Data.Likes.Delete(yourPositiveLike);
                }
            }

            Like newLike = new Like
            {
                LikedById = currentUserId,
                WorkId = like.WorkId,
            };
            if (IfCanLike)
            {
                if (like.LikeState == "like")
                {
                    newLike.IsPositive = true;
                }
                else
                {
                    newLike.IsPositive = false;
                }
                this.Data.Likes.Add(newLike);
                this.Data.SaveChanges();
            }

            return workVotedFor;
        }

        private bool CheckIfUserCanLikeOrDislike(int workVotedForId, string currentUserId, string likeOrDislike)
        {
            switch (likeOrDislike.ToLower())
            {
                case "like": return !this.Data.Likes.All()
                .Any(x => x.WorkId == workVotedForId && x.LikedById == currentUserId && x.IsPositive);

                case "dislike": return !this.Data.Likes.All()
                .Any(x => x.WorkId == workVotedForId && x.LikedById == currentUserId && !x.IsPositive);

                default: return false;
            }
        }
    }
}