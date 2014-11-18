namespace DigitalLibrary.Web.Areas.Administration.ViewModels.Authors
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Base;
    using DigitalLibrary.Web.Infrastructure.Mapping;

    public class AuthorViewModel : AdministrationViewModel, IMapFrom<Author>, ISimpleView<SimpleViewModel>
    {
        public static Expression<Func<Author, AuthorViewModel>> FromAuthor
        {
            get
            {
                return a => new AuthorViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    CreatedOn = a.CreatedOn,
                    ModifiedOn = a.ModifiedOn,
                };
            }
        }
  
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Required]
        [MaxLength(100)]
        [UIHint("SingleLineText")]
        [Display(Name = "Name")]
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }
}