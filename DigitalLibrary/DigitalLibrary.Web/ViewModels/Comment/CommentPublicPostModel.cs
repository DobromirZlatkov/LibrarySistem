namespace DigitalLibrary.Web.ViewModels.Comment
{
    using System;

    public class CommentPublicPostModel
    {
        public string Content { get; set; }
            
        public string PostedBy{ get; set; }

        public int WorkId { get; set; }
    }
}