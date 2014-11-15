namespace DigitalLibrary.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using DigitalLibrary.Data;
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.ViewModels.Like;

    public class LikePublicController : BaseController
    {
        public LikePublicController(IDigitalLibraryData data)
            : base(data)
        {
        }

        public ActionResult Action(LikeSubmitModel like)
        {
            var currentUserId = this.CurrentUser.Id;

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

            Like newLike;
            if (IfCanLike)
            {
                if (like.LikeState == "like")
                {
                    newLike = new Like
                    {
                        LikedById = currentUserId,
                        WorkId = like.WorkId,
                        IsPositive = true
                    };
                }
                else
                {
                    newLike = new Like
                    {
                        LikedById = currentUserId,
                        WorkId = like.WorkId,
                        IsPositive = false
                    };
                }
                this.Data.Likes.Add(newLike);
            }

            var workVotedFor = this.Data.Works.GetById(like.WorkId);
            var positiveMinusNegativeLikes = CalculateCountRate(workVotedFor);
            return this.Content(positiveMinusNegativeLikes.ToString());
        }

        private static int CalculateCountRate(Work workVotedFor)
        {
            var positive = workVotedFor.Likes.Count(l => l.IsPositive);
            var negative = workVotedFor.Likes.Count(l => !l.IsPositive);

            return positive - negative;
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