namespace DigitalLibrary.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using DigitalLibrary.Data;
    using DigitalLibrary.Models;

    public class LikePublicController : BaseController
    {
        public LikePublicController(IDigitalLibraryData data)
            : base(data)
        {
        }

        public ActionResult Like(int id)
        {
            var workVotedFor = this.Data.Works.GetById(id);

            var currentUserId = User.Identity.GetUserId();

            var canLike = CheckIfUserCanLikeOrDislike(workVotedFor, currentUserId, "like");

            var yourNegativeLike = this.Data.Likes.All()
                .Where(x => x.WorkId == workVotedFor.Id && x.LikedById == currentUserId && !x.IsPositive)
                .FirstOrDefault();

            if (canLike)
            {
                var newLike = new Like
                {
                    LikedById = currentUserId,
                    WorkId = workVotedFor.Id,
                    IsPositive = true
                };

                workVotedFor.Likes.Add(newLike);
            }
            if (yourNegativeLike != null)
            {
                this.Data.Likes.Delete(yourNegativeLike);
            }

            this.Data.SaveChanges();


            var positiveMinusNegativeLikes = CalculateCountRate(workVotedFor);

            return this.Content(positiveMinusNegativeLikes.ToString());
        }

        public ActionResult Dislike(int id)
        {
            var workVotedFor = this.Data.Works.GetById(id);
            var currentUserId = User.Identity.GetUserId();

            var canDislike = CheckIfUserCanLikeOrDislike(workVotedFor, currentUserId, "dislike");

            var yourPositiveLike = this.Data.Likes.All()
                .Where(x => x.WorkId == id && x.LikedById == currentUserId && x.IsPositive)
                .FirstOrDefault();

            if (canDislike)
            {
                var newLike = new Like
                {
                    LikedById = currentUserId,
                    WorkId = workVotedFor.Id,
                    IsPositive = false
                };

                workVotedFor.Likes.Add(newLike);
            }
            if (yourPositiveLike != null)
            {
                this.Data.Likes.Delete(yourPositiveLike);
            }
            this.Data.SaveChanges();

            var positiveMinusNegativeLikes = CalculateCountRate(workVotedFor);

            return this.Content(positiveMinusNegativeLikes.ToString());
        }

        private static int CalculateCountRate(Work workVotedFor)
        {
            var positive = workVotedFor.Likes.Count(l => l.IsPositive);
            var negative = workVotedFor.Likes.Count(l => !l.IsPositive);

            return positive - negative;
        }

        private bool CheckIfUserCanLikeOrDislike(Work workVotedFor, string currentUserId, string likeOrDislike)
        {
            switch (likeOrDislike.ToLower())
            {
                case "like": return !this.Data.Likes.All()
                .Any(x => x.WorkId == workVotedFor.Id && x.LikedById == currentUserId && x.IsPositive);

                case "dislike": return !this.Data.Likes.All()
                .Any(x => x.WorkId == workVotedFor.Id && x.LikedById == currentUserId && !x.IsPositive);

                default: return false;
            }
        }
    }
}