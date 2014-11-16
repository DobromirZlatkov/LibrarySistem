namespace DigitalLibrary.Web.Areas.Administration.ViewModels.Works
{
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Base;
    using DigitalLibrary.Web.Infrastructure.Mapping;

    public class WorkEditModel : AdministrationViewModel, IMapFrom<Work>, IMapFrom<WorkViewModel>, IHaveCustomMappings
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public bool IsApproved { get; set; }

        public string ZipFileLink { get; set; }

        public string PictureLink { get; set; }

        public int AuthorId { get; set; }

        public int GenreId { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<WorkViewModel, WorkEditModel>()
                .ForMember(m => m.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(m => m.Title, opt => opt.MapFrom(c => c.Title))
                .ForMember(m => m.IsApproved, opt => opt.MapFrom(c => c.IsApproved))
                .ForMember(m => m.ZipFileLink, opt => opt.MapFrom(c => c.ZipFileLink))
                .ForMember(m => m.PictureLink, opt => opt.MapFrom(c => c.PictureLink))
                .ReverseMap();
        }
    }
}