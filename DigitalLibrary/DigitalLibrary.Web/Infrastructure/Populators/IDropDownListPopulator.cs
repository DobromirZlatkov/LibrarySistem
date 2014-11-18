namespace DigitalLibrary.Web.Infrastructure.Populators
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public interface IDropDownListPopulator
    {
        IEnumerable<SelectListItem> GetAllGenres();

        IEnumerable<SelectListItem> GetAllAuthors();

        IEnumerable<SelectListItem> GetYears();
    }
}