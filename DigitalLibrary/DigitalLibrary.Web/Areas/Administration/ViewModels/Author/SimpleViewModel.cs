namespace DigitalLibrary.Web.Areas.Administration.ViewModels.Authors
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Mvc;

    using DigitalLibrary.Web.Infrastructure.Mapping;

    public class SimpleViewModel : IMapFrom<AuthorViewModel>
    {
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
