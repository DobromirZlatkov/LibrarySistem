namespace DigitalLibrary.Web.ViewModels.Work
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using DigitalLibrary.Models;
    using DigitalLibrary.Web.ViewModels.Comment;

    public class WorkPublicDetailsViewModel
    {
        public static Expression<Func<Work, WorkPublicDetailsViewModel>> FromWork
        {
            get
            {
                return w => new WorkPublicDetailsViewModel
                {
                    Id = w.Id,
                    Title = w.Title,
                    Description = w.Description,
                    Year = w.Year,
                    ZipFileLink = w.ZipFileLink,
                    PictureLink = w.PictureLink,
                    AuthorId = w.AuthorId,
                    AuthorName = w.Author.Name,
                    UploadedBy = w.UploadedBy.UserName,
                    Genre = w.Genre.GenreName,
                    PositiveLikes = w.Likes.Count(l => l.IsPositive),
                    NegativeLikes = w.Likes.Count(l => !l.IsPositive),
                    Comments = w.Comments.AsQueryable().Where(c => !c.IsDeleted).Select(CommentPublicViewModel.FromComment)
                };
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public string ZipFileLink { get; set; }

        public string PictureLink { get; set; }

        public int AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string UploadedBy { get; set; }

        public string Genre { get; set; }

        private int PositiveLikes { get; set; }

        private int NegativeLikes { get; set; }

        public IEnumerable<CommentPublicViewModel> Comments { get; set; }

        public int LikesCount
        {
            get
            {
                return this.PositiveLikes - this.NegativeLikes;
            }
        }
   
    }
}