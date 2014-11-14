namespace DigitalLibrary.Data.Migrations
{
    using DigitalLibrary.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DigitalLibraryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DigitalLibraryDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }
            context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
            {
                Name = "trusted"
            });
            context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
            {
                Name = "admin"
            });
            context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
            {
                Name = "non-trusted"
            });
            context.SaveChanges();

        }
    }
}
