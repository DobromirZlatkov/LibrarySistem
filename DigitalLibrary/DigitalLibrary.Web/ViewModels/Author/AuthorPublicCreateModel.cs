namespace DigitalLibrary.Web.ViewModels.Authors
{
    using System.ComponentModel.DataAnnotations;

    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Infrastructure.Mapping;

    public class AuthorPublicCreateModel : IMapFrom<Author>
    {
        public int? Id { get; set; }

        [UIHint("SingleLineText")]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}