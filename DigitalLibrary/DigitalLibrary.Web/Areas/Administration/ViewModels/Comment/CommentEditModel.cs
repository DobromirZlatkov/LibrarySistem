using DigitalLibrary.Models;
using DigitalLibrary.Web.Areas.Administration.ViewModels.Base;
using DigitalLibrary.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DigitalLibrary.Web.Areas.Administration.ViewModels.Comments
{
    public class CommentEditModel : AdministrationViewModel, IMapFrom<Comment>, ISimpleView<CommentViewModel>
    {
        public int? Id { get; set; }

        public string Content { get; set; }
    }
}