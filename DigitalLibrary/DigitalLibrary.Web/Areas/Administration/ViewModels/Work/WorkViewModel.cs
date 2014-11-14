namespace DigitalLibrary.Web.Areas.Administration.ViewModels.Works
{

 
    using System.Web.Mvc;

    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Base;
    using DigitalLibrary.Web.Infrastructure.Mapping;
    using System.Linq.Expressions;
    using System;
   

    public class WorkViewModel : AdministrationViewModel, IMapFrom<Work>
    {
        public static Expression<Func<Work, WorkViewModel>> FromWork
        {
            get
            {
                return a => new WorkViewModel
                {
                   Id = a.Id,
                   Title = a.Title,
                   Description = a.Description,
                   Year = a.Year,
                   IsApproved = a.IsApproved,
                   PictureLink = a.PictureLink,
                   Author = a.Author.Name,
                   UploadedBy = a.UploadedBy.UserName,
                   Genre = a.Genre.GenreName,
                   CreatedOn = a.CreatedOn,
                   ModifiedOn = a.ModifiedOn, 
                };
            }
        }

        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public bool IsApproved { get; set; }

        public string PictureLink { get; set; }

        public string Author { get; set; }

        public string UploadedBy { get; set; }

        public string Genre { get; set; }
    }
}