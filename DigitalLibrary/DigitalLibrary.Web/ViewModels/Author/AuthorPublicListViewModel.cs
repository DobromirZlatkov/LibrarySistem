namespace DigitalLibrary.Web.ViewModels.Author
{
    using System;
    using System.Linq.Expressions;

    using DigitalLibrary.Models;

    public class AuthorPublicListViewModel
    {
        public static Expression<Func<Author, AuthorPublicListViewModel>> FromAuthor
        {
            get
            {
                return a => new AuthorPublicListViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}