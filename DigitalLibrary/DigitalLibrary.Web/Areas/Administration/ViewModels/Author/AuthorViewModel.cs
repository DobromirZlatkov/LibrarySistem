using DigitalLibrary.Web.Areas.Administration.ViewModels.Author;
namespace DigitalLibrary.Web.Areas.Administration.ViewModels.Authors
{
    using System.Linq.Expressions;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Base;
    using DigitalLibrary.Web.Infrastructure.Mapping;
    using System.ComponentModel.DataAnnotations.Schema;

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

        //public AuthorViewModel()
        //{
        //   // this.Works = new HashSet<Work>();
        //}

        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

       // public ICollection<Work> Works { get; set; }
    }
}