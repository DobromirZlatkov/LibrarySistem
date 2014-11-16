namespace DigitalLibrary.Data
{
    using DigitalLibrary.Data.Contracts;
    using DigitalLibrary.Data.Contracts.CodeFirstConventions;
    using DigitalLibrary.Data.Migrations;
    using DigitalLibrary.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using System.Web.Mvc;
    using System.Data.Entity.Validation;

    public class DigitalLibraryDbContext : IdentityDbContext<User>, IDigitalLibraryDbContext
    {
        public DigitalLibraryDbContext()
            : this("DefaultConnection")
        {
        }

        public DigitalLibraryDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DigitalLibraryDbContext, Configuration>());
        }

        public virtual IDbSet<Author> Authors { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Genre> Genres { get; set; }

        public virtual IDbSet<Like> Likes { get; set; }

        public virtual IDbSet<Work> Works { get; set; }

        public DbContext DbContext
        {
            get
            {
                return this;
            }
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            this.ApplyDeletableEntityRules();
            return base.SaveChanges();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public static DigitalLibraryDbContext Create()
        {
            return new DigitalLibraryDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new IsUnicodeAttributeConvention());

            //modelBuilder.Entity<Author>()
            //    .HasMany(w =>w.Works)
            //    .WithOptional(i => i.Author)
            //    .HasForeignKey(i => i.AuthorId)
            // .WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void ApplyAuditInfoRules()
        {
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    //var format = "yyyy-MM-dd HH:mm:ss:fff";
                    //var stringDate = DateTime.Now.ToString(format);
                    //entity.ModifiedOn = DateTime.ParseExact(stringDate, format, CultureInfo.InvariantCulture);
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}
