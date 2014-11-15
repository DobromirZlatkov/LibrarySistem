namespace DigitalLibrary.Web.Areas.Administration.ViewModels.Works
{
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Base;
    using DigitalLibrary.Web.Infrastructure.Mapping;

    public class WorkEditModel : AdministrationViewModel, IMapFrom<Work>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public bool IsApproved { get; set; }

        public string ZipFileLink { get; set; }

        public string PictureLink { get; set; }

        public int AuthorId { get; set; }

        public int GenreId { get; set; }
    }
}