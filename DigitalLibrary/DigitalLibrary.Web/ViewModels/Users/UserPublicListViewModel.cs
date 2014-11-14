namespace DigitalLibrary.Web.ViewModels.Users
{ 
    ﻿using DigitalLibrary.Models;
    using System;
    using System.Linq.Expressions;

    public class UserPublicListViewModel

    {
        public static Expression<Func<User, UserPublicListViewModel>> FromUser
        {
            get
            {
                return u => new UserPublicListViewModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                };
            }
        }

        public string Id { get; set; }

        public string UserName { get; set; }
    }
}
