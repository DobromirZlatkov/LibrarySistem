using DigitalLibrary.Models;
using DigitalLibrary.Web.Areas.Administration.ViewModels.Base;
using DigitalLibrary.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace DigitalLibrary.Web.Areas.Administration.ViewModels.Users
{
    public class UserViewModel : AdministrationViewModel, IMapFrom<User>
    {
        public static Expression<Func<User, UserViewModel>> FromUser
        {
            get
            {
                return a => new UserViewModel
                {
                    Id = a.Id,
                    Email = a.Email,
                    AccessFailedCount = a.AccessFailedCount,
                    LockoutEnabled = a.LockoutEnabled,
                    NegativeUploads = a.NegativeUploads,
                    PositiveUploads = a.PositiveUploads,
                    Roles = a.Roles.FirstOrDefault().RoleId,
                    UserName = a.UserName
                };
            }
        }

        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        public string Email { get; set; }

        public int AccessFailedCount { get; set; }

        public bool LockoutEnabled { get; set; }

        public int NegativeUploads { get; set; }

        public int PositiveUploads { get; set; }

        public string Roles { get; set; }

        public string UserName { get; set; }
    }
}