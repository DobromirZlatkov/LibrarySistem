namespace DigitalLibrary.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using DigitalLibrary.Data.Contracts;
    using DigitalLibrary.Data.Contracts.CodeFirstConventions;
    using DigitalLibrary.Data.Migrations;
    using DigitalLibrary.Models;

    using Microsoft.AspNet.Identity.EntityFramework;
 
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

        public static DigitalLibraryDbContext Create()
        {
            return new DigitalLibraryDbContext();
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new IsUnicodeAttributeConvention());

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
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        { 
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
