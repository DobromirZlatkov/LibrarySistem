namespace DigitalLibrary.Web.ViewModels.Work
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;
    using System.Reflection;
    using System.IO;

    using DigitalLibrary.Models;
   

    public class WorkPublicListViewModel
    {
        public static Expression<Func<Work, WorkPublicListViewModel>> FromWork
        {
            get
            {
                return w => new WorkPublicListViewModel
                {
                    Id = w.Id,
                    Title = w.Title,
                    Year = w.Year,
                    ZipFileLink = w.ZipFileLink,
                    PictureLink = w.PictureLink,
                    AuthorName = w.Author.Name,
                    AuthorId = w.AuthorId,
                    PositiveLikes = w.Likes.Count(l => l.IsPositive),
                    NegativeLikes = w.Likes.Count(l => !l.IsPositive),
                };
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public int Year { get; set; }

        public string ZipFileLink { get; set; }

        public string PictureLink { get; set; }

        public string AuthorName { get; set; }

        public int AuthorId { get; set; }

        private int PositiveLikes { get; set; }

        private int NegativeLikes { get; set; }

        public int LikesCount
        {
            get
            {
                return this.PositiveLikes - this.NegativeLikes;
            }
        }

    }
}