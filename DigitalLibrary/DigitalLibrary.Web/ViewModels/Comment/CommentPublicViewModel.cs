namespace DigitalLibrary.Web.ViewModels.Comment
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq.Expressions;

    using DigitalLibrary.Models;

    public class CommentPublicViewModel
    {
        public static Expression<Func<Comment, CommentPublicViewModel>> FromComment
        {
            get
            {
                return w => new CommentPublicViewModel
                {
                    Content = w.Content,
                    PostedBy = w.PostedBy.UserName,
                    DatePosted = w.DatePosted
                };
            }
        }

        public string Content { get; set; }

        public string PostedBy { get; set; }

        [Column(TypeName = "DateTime")]
        public DateTime DatePosted { get; set; }
    }
}