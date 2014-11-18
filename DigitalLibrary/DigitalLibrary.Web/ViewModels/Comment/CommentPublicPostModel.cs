namespace DigitalLibrary.Web.ViewModels.Comment
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CommentPublicPostModel
    {
        [StringLength(1000, MinimumLength = 5, ErrorMessage = "Use 5-1000 characters")]
        [UIHint("MultiLineText")]
        public string Content { get; set; }
            
        public string PostedBy{ get; set; }

        public int WorkId { get; set; }
    }
}