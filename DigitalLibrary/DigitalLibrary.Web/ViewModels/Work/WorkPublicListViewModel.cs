namespace DigitalLibrary.Web.ViewModels.Work
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;
    using System.Reflection;
    using System.IO;

    using DigitalLibrary.Models;
    using System.ComponentModel.DataAnnotations;
   

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
                    IsApproved = w.IsApproved
                };
            }
        }

        [Editable(false)]
        public int Id { get; set; }

        [Editable(false)]
        public string Title { get; set; }

        [Editable(false)]
        public int Year { get; set; }

        [Editable(false)]
        public string ZipFileLink { get; set; }

        [Editable(false)]
        public string PictureLink { get; set; }

        [Editable(false)]
        public string AuthorName { get; set; }

        [Editable(false)]
        public int AuthorId { get; set; }

        [Editable(true)]
        public bool IsApproved { get; set; }

        [Editable(false)]
        private int PositiveLikes { get; set; }

        [Editable(false)]
        private int NegativeLikes { get; set; }


        [Editable(false)]
        public int LikesCount
        {
            get
            {
                return this.PositiveLikes - this.NegativeLikes;
            }
        }

    }
}