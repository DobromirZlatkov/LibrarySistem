
namespace DigitalLibrary.Web.Infrastructure.Services
{
    using System.Linq;

    using DigitalLibrary.Data;
    using DigitalLibrary.Web.Controllers;
    using DigitalLibrary.Web.Infrastructure.Services.Contracts;
    using DigitalLibrary.Web.ViewModels.Genre;

    public class WorkService : BaseController, IWorkService
    {
        public WorkService(IDigitalLibraryData data)
        : base(data)
        {
        }

        public IQueryable<GenrePublicViewModel> GetAllGenres()
        {
            return this.Data.Genres.All().Select(GenrePublicViewModel.FromGenre);
        }
    }
}