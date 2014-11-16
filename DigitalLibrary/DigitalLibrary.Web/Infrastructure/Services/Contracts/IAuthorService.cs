namespace DigitalLibrary.Web.Infrastructure.Services.Contracts
{
    using System.Linq;

    using DigitalLibrary.Web.ViewModels.Authors;

    public interface IAuthorService
    {
        IQueryable<AuthorPublicListViewModel> GetAuthors();
    }
}