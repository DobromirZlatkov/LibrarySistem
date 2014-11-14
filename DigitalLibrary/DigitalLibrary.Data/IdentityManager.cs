namespace DigitalLibrary.Data
{
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using DigitalLibrary.Models;

    public class IdentityManager
    {
        public bool RoleExists(string name)
        {
            var rm = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new DigitalLibraryDbContext()));
            return rm.RoleExists(name);
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var um = new UserManager<User>(
                new UserStore<User>(new DigitalLibraryDbContext()));
            var idResult = um.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }

        public void ClearUserRoles(string userId, string role)
        {
            var um = new UserManager<User>(
                new UserStore<User>(new DigitalLibraryDbContext()));
            var user = um.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();
            currentRoles.AddRange(user.Roles);

            um.RemoveFromRole(userId, role);
        }
    }
}
