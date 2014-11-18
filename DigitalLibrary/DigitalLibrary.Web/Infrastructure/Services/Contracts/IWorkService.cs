namespace DigitalLibrary.Web.Infrastructure.Services.Contracts
{
    using System.Linq;

    using DigitalLibrary.Web.ViewModels.Genre;

    public interface IWorkService
    {
        IQueryable<GenrePublicViewModel> GetAllGenres();
    }
}
