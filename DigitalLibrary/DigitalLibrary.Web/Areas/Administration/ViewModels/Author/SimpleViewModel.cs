using DigitalLibrary.Web.Areas.Administration.ViewModels.Authors;
using DigitalLibrary.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace DigitalLibrary.Web.Areas.Administration.ViewModels.Author
{
    public class SimpleViewModel : IMapFrom<AuthorViewModel>
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
    }
}
