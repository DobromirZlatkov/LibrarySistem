namespace DigitalLibrary.Web.Infrastructure.Services.Contracts
{
    using System.Collections.Generic;

    using DigitalLibrary.Web.ViewModels.Genre;
    using DigitalLibrary.Web.ViewModels.Home;
    using DigitalLibrary.Web.ViewModels.Users;
    using DigitalLibrary.Web.ViewModels.Work;

    public interface IHomeServices
    {
        IList<UserPublicListViewModel> GetTopUsers(int numberOfUsers);

        HomePageStatisticsModel GetStatistics();

        IList<WorkPublicListViewModel> GetTopRatedWorks(int numberOfWorks);

        IList<GenrePublicViewModel> AllGenres();
    }
}