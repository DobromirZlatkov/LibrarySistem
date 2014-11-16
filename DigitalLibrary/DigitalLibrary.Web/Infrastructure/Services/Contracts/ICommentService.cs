using DigitalLibrary.Models;
using DigitalLibrary.Web.ViewModels.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalLibrary.Web.Infrastructure.Services.Contracts
{
    public interface ICommentService
    {
        CommentPublicViewModel Create(CommentPublicPostModel commentModel, User CurrentUser);
    }
}