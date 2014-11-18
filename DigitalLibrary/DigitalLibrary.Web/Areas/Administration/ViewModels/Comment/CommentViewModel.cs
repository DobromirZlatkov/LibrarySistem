namespace DigitalLibrary.Web.Areas.Administration.ViewModels.Comments
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Areas.Administration.ViewModels.Base;
    using DigitalLibrary.Web.Infrastructure.Mapping;

    public class CommentViewModel : AdministrationViewModel, IMapFrom<Comment>
    {
        public static Expression<Func<Comment, CommentViewModel>> FromComment
        {
            get
            {
                return a => new CommentViewModel
                {
                    Id = a.Id,
                    Content = a.Content,
                    DatePosted = a.DatePosted,
                    PostedBy = a.PostedBy.UserName,
                    Work = a.Work.Title,
                    CreatedOn = a.CreatedOn,
                    ModifiedOn = a.ModifiedOn,
                };
            }
        }

        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        [Editable(true)]
        [UIHint("MultiLinetext")]
        [Display(Name = "Content")]
        public string Content { get; set; }

        [Editable(false)]
        [HiddenInput(DisplayValue = false)]
        [Column(TypeName = "DateTime2")]
        public DateTime DatePosted { get; set; }

        [Editable(false)]
        [HiddenInput(DisplayValue = false)]
        public string PostedBy { get; set; }

        [Editable(false)]
        [HiddenInput(DisplayValue = false)]
        public string Work { get; set; }
    }
}