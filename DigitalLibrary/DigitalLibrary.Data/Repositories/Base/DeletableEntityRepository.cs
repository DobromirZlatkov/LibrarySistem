namespace DigitalLibrary.Data.Repositories.Base
{
    using DigitalLibrary.Data.Contracts;
    using System;
    using System.Data.Entity;
    using System.Globalization;
    using System.Linq;

    public class DeletableEntityRepository<T> :
    GenericRepository<T>, IDeletableEntityRepository<T> where T : class, IDeletableEntity
    {
        public DeletableEntityRepository(IDigitalLibraryDbContext context)
            : base(context)
        {
        }

        public override IQueryable<T> All()
        {
            return base.All().Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }

        public override void Delete(T entity)
        {
            var format = "yyyy-MM-dd HH:mm:ss:fff";
            string date = DateTime.Now.ToString(format);

            entity.DeletedOn = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
            entity.IsDeleted = true;

            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Modified;
        }
    }
}
