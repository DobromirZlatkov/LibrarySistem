namespace DigitalLibrary.Web.ViewModels.Genre
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using DigitalLibrary.Models;
    using DigitalLibrary.Web.ViewModels.Work;

    public class GenrePublicViewModel
    {
        public static Expression<Func<Genre, GenrePublicViewModel>> FromGenre
        {
            get
            {
                return g => new GenrePublicViewModel
                {
                    Id = g.Id,
                    GenreName = g.GenreName,
                    Works = g.Works.AsQueryable()
                                    .Where(w => w.IsApproved)
                                    .Select(WorkPublicListViewModel.FromWork)
                };
            }
        }

        public int Id { get; set; }

        public string GenreName { get; set; }

        public IEnumerable<WorkPublicListViewModel> Works { get; set; }
    }
}