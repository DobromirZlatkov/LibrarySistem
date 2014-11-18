namespace DigitalLibrary.Web.Infrastructure.Populators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using DigitalLibrary.Data;
    using DigitalLibrary.Web.Infrastructure.Cashing;

    public class DropDownListPopulator : IDropDownListPopulator
    {
        private const int StartWorkYear = 1800;
        private int CurrentYear = DateTime.Now.Year;
        private IDigitalLibraryData data;
        private ICacheService cache;

        public DropDownListPopulator(IDigitalLibraryData data, ICacheService cache)
        {
            this.cache = cache;
            this.data = data;
        }

        public IEnumerable<SelectListItem> GetAllGenres()
        {
            var genres = this.cache.Get<IEnumerable<SelectListItem>>("genres",
           () =>
           {
               return this.data.Genres
                  .All()
                  .Select(c => new SelectListItem
                  {
                      Value = c.Id.ToString(),
                      Text = c.GenreName
                  })
                  .ToList();
           });

            return genres;
        }

        public IEnumerable<SelectListItem> GetAllAuthors()
        {
            return this.data.Authors
               .All()
               .Select(c => new SelectListItem
               {
                   Value = c.Id.ToString(),
                   Text = c.Name
               })
               .ToList();
        }

        public IEnumerable<SelectListItem> GetYears()
        {
            var possibleYears = this.cache.Get<IEnumerable<SelectListItem>>("years",
             () =>
             {
                 var years = new List<SelectListItem>();
                 for (int i = StartWorkYear; i <= CurrentYear; i++)
			    {
			        years.Add(new SelectListItem
                                {
                                    Value =i.ToString(),
                                    Text = i.ToString()
                                });
                 }

                 return years;
             });

            return possibleYears;
        }
    }
}