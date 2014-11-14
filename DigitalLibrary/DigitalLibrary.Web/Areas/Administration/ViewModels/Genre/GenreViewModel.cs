namespace DigitalLibrary.Web.Areas.Administration.ViewModels.Genres
{
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Base;
    using DigitalLibrary.Web.Infrastructure.Mapping;
    using System.Linq.Expressions;
    using System;

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

        [Display(Name = "Име")]
     //   [Required]
        //[StringLength(100, MinimumLength = 3)]
        public string GenreName { get; set; }
    }
}