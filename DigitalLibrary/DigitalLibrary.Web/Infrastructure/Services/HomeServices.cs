namespace DigitalLibrary.Web.Infrastructure.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using DigitalLibrary.Data;
    using DigitalLibrary.Web.Controllers;
    using DigitalLibrary.Web.Infrastructure.Services.Contracts;
    using DigitalLibrary.Web.ViewModels.Genre;
    using DigitalLibrary.Web.ViewModels.Home;
    using DigitalLibrary.Web.ViewModels.Users;
    using DigitalLibrary.Web.ViewModels.Work;

    public class HomeServices : BaseController, IHomeServices
    {
        public HomeServices(IDigitalLibraryData data)
            : base(data)
        {
        }

        public IList<UserPublicListViewModel> GetTopUsers(int numberOfUsers)
        {
            var topUsers = this.Data.Users.All()
                  .OrderByDescending(u => u.PositiveUploads)
                  .Select(UserPublicListViewModel.FromUser)
                  .Take(numberOfUsers).ToList();

            return topUsers;
        }

        public HomePageStatisticsModel GetStatistics()
        {
            var allAuthorsForStatistics = this.Data.Authors.All().Count();
            var allUsersForStatistics = this.Data.Users.All().Count();
            var allGenresForStatistics = this.Data.Genres.All().Count();
            var allWorksForStatistics = this.Data.Works.All().Count();

            var statistics = new HomePageStatisticsModel
            {
                NumberOfAuthors = allAuthorsForStatistics,
                NumberOfGenres = allGenresForStatistics,
                NumberOfUsers = allUsersForStatistics,
                NumberOfWorks = allWorksForStatistics
            };

            return statistics;
        }

        public IList<WorkPublicListViewModel> GetTopRatedWorks(int numberOfWorks)
        {
            var mostPopularWorks = this.Data.Works.All()
                .Where(w => w.IsApproved)
                .OrderByDescending(w => w.Likes.Count)
                .Select(WorkPublicListViewModel.FromWork)
                .Take(numberOfWorks).ToList();

            return mostPopularWorks;
        }

        public IList<GenrePublicViewModel> AllGenres()
        {
            var genres = this.Data.Genres
                .All()
                .Select(GenrePublicViewModel.FromGenre)
                .ToList();

            return genres;
        }
    }
}