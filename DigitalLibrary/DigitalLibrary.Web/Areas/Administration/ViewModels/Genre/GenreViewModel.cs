namespace DigitalLibrary.Web.Areas.Administration.ViewModels.Genres
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Base;
    using DigitalLibrary.Web.Infrastructure.Mapping;

    public class GenreViewModel : AdministrationViewModel, IMapFrom<Genre>
    {
        public static Expression<Func<Genre, GenreViewModel>> FromGenreAdmin
        {
            get
            {
                return a => new GenreViewModel
                {
                    Id = a.Id,
                    GenreName = a.GenreName,
                    CreatedOn = a.CreatedOn,
                    ModifiedOn = a.ModifiedOn,                    
                };
            }
        }

        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [UIHint("SingleLineText")]
        [Display(Name = "Name")]
        public string GenreName { get; set; }
    }
}