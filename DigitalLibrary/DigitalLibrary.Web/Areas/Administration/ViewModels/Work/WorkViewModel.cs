namespace DigitalLibrary.Web.Areas.Administration.ViewModels.Works
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Base;

    public class WorkViewModel : AdministrationViewModel
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
                   ZipFileLink = a.ZipFileLink,
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

        [UIHint("SingleLineText")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [UIHint("MultiLinetext")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [UIHint("Integer")]
        [Display(Name = "Year")]
        public int Year { get; set; }

        [Display(Name = "IsApproved")]
        public bool IsApproved { get; set; }

        public string PictureLink { get; set; }

        public string ZipFileLink { get; set; }

        [Editable(false)]
        public string Author { get; set; }

        [Editable(false)]
        public string UploadedBy { get; set; }

        public string Genre { get; set; }
    }
}