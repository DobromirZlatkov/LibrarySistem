using DigitalLibrary.Models;
using DigitalLibrary.Web.Infrastructure.Mapping;
namespace DigitalLibrary.Web.ViewModels.Authors
{
    public class AuthorPublicCreateModel : IMapFrom<Author>
    {
        public int? Id { get; set; }

        public string Name { get; set; }
    }
}