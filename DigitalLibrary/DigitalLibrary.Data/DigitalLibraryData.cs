namespace DigitalLibrary.Data
{
    using System;
    using System.Collections.Generic;

    using DigitalLibrary.Data.Contracts;
    using DigitalLibrary.Data.Repositories.Base;
    using DigitalLibrary.Models;

    public class DigitalLibraryData : IDigitalLibraryData
    {
        private readonly IDigitalLibraryDbContext context;

        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public DigitalLibraryData(IDigitalLibraryDbContext context)
        {
            this.context = context;
        }

        public IDigitalLibraryDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IDeletableEntityRepository<Author> Authors
        {
            get { return this.GetDeletableEntityRepository<Author>(); }
        }

        public IDeletableEntityRepository<Comment> Comments
        {
            get { return this.GetDeletableEntityRepository<Comment>(); }
        }

        public IDeletableEntityRepository<Genre> Genres
        {
            get { return this.GetDeletableEntityRepository<Genre>(); }
        }

        public IRepository<Like> Likes
        {
            get { return this.GetRepository<Like>(); }
        }

        public IDeletableEntityRepository<Work> Works
        {
            get { return this.GetDeletableEntityRepository<Work>(); }
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        /// The number of objects written to the underlying database.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">Thrown if the context has been disposed.</exception>
        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }
    }
}
