namespace DigitalLibrary.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using DigitalLibrary.Data;
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Infrastructure.Services.Contracts;
    using DigitalLibrary.Web.ViewModels.Like;
    using DigitalLibrary.Web.ViewModels.Work;

    public class LikePublicController : BaseController
    {
        private ILikeService likeServices;

        public LikePublicController(IDigitalLibraryData data, ILikeService likeServices)
            : base(data)
        {
            this.likeServices = likeServices;
        }

        public ActionResult GetLikes(LikeSubmitModel like)
        {
            var work = this.Data.Works
                .All()
                .Where(w => w.Id == like.WorkId).Select(WorkPublicDetailsViewModel.FromWork)
                .FirstOrDefault();

            var viewModel = new LikeViewModel();
            viewModel.WorkId = work.Id;
            viewModel.LikeCount = work.LikesCount;

            return this.PartialView("_LikeControllerPartial", viewModel);
        }

        public ActionResult Action(LikeSubmitModel like)
        {
            var workVotedFor = this.likeServices.ExtecuteLikeOrDislikeAction(like, this.CurrentUser);
            var positiveMinusNegativeLikes = CalculateCountRate(workVotedFor);

            return this.Content(positiveMinusNegativeLikes.ToString());
        }

        private static int CalculateCountRate(Work workVotedFor)
        {
            var positive = workVotedFor.Likes.Count(l => l.IsPositive);
            var negative = workVotedFor.Likes.Count(l => !l.IsPositive);

            return positive - negative;
        }
    }
}