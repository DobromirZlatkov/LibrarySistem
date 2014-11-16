namespace DigitalLibrary.Data
{
    using DigitalLibrary.Data.Contracts;
    using DigitalLibrary.Models;

    public interface IDigitalLibraryData
    {
        IDigitalLibraryDbContext Context { get; }

        IDeletableEntityRepository<Author> Authors { get; }

        IDeletableEntityRepository<Comment> Comments { get; }

        IDeletableEntityRepository<Genre> Genres { get; }

        IRepository<Like> Likes { get; }

        IDeletableEntityRepository<Work> Works { get; }

        IRepository<User> Users { get; }

        int SaveChanges();
    }
}
