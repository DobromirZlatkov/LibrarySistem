namespace DigitalLibrary.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using DigitalLibrary.Web.ViewModels.Genre;
    using DigitalLibrary.Web.ViewModels.Users;
    using DigitalLibrary.Web.ViewModels.Work;

    public class HomePageModel
    {
        public IEnumerable<WorkPublicListViewModel> MostPopularWorks { get; set; }

        public IEnumerable<GenrePublicViewModel> GenreBooks { get; set; }

        public HomePageStatisticsModel Statistics { get; set; }

        public IEnumerable<UserPublicListViewModel> TopUsers { get; set; }
    }
}