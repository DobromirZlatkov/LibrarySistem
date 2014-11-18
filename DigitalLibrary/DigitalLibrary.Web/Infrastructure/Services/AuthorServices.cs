namespace DigitalLibrary.Web.Infrastructure.Services
{
    using System.Linq;

    using DigitalLibrary.Data;
    using DigitalLibrary.Web.Controllers;
    using DigitalLibrary.Web.Infrastructure.Services.Contracts;
    using DigitalLibrary.Web.ViewModels.Authors;

    public class AuthorServices : BaseController, IAuthorService
    {
        public AuthorServices(IDigitalLibraryData data)
            : base(data)
        {
        }

        public IQueryable<AuthorPublicListViewModel> GetAuthors()
        {
            var allAuthors = this.Data.Authors
              .All()
              .Select(AuthorPublicListViewModel.FromAuthor);

            return allAuthors;
        }
    }
}