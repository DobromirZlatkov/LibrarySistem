using AutoMapper;
using DigitalLibrary.Models;
using DigitalLibrary.Web.Areas.Administration.ViewModels.Base;
using DigitalLibrary.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalLibrary.Web.Areas.Administration.ViewModels.Comments
{
    public class CommentEditModel : AdministrationViewModel, IMapFrom<Comment>, IMapFrom<CommentViewModel>, IHaveCustomMappings
    {
        public int? Id { get; set; }

        public string Content { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<CommentViewModel, CommentEditModel>()
                .ForMember(m => m.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(m => m.Content, opt => opt.MapFrom(c => c.Content))
                .ForMember(m => m.CreatedOn, opt => opt.MapFrom(c => c.CreatedOn))
                .ForMember(m => m.ModifiedOn, opt => opt.MapFrom(c => c.ModifiedOn))
                .ReverseMap();
        }

    }
}