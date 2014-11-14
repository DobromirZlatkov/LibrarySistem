namespace DigitalLibrary.Web.Controllers
{

    using DigitalLibrary.Data;
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.ViewModels.Author;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;


    public class AuthorController : BaseController
    {
        public AuthorController(IDigitalLibraryData data)
            : base(data)
        {
        }


       public ActionResult Create(AuthorPublicCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var newAuthor = new Author
                {
                    Name = model.AuthorName
                };

                this.Data.Authors.Add(newAuthor);
                this.Data.SaveChanges();
              

            }

            Response.Redirect("~/WorkPublic/Create");//to return authordropdown list view

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ModelState.Values.First().ToString());
        }


        private IQueryable<AuthorPublicListViewModel> GetAllAuthors()
        {
            var allAuthors = this.Data.Authors
                .All()
                .Where(a => !a.IsDeleted)
                .Select(AuthorPublicListViewModel.FromAuthor);

            return allAuthors;
        }

    }
}