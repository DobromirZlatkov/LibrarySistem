namespace DigitalLibrary.Data
{
    using DigitalLibrary.Models;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface IDigitalLibraryDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Author> Authors { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<Genre> Genres { get; set; }

        IDbSet<Like> Likes { get; set; }

        IDbSet<Work> Works { get; set; }

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
