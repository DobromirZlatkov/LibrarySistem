namespace DigitalLibrary.Web.Infrastructure.Services
{
    using System.Linq;

    using DigitalLibrary.Web.Areas.Administration.Controllers.Base;
    using DigitalLibrary.Web.Infrastructure.Services.Contracts;
    using DigitalLibrary.Web.ViewModels.Authors;
    using DigitalLibrary.Data;
    using DigitalLibrary.Web.Controllers;

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