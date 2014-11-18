namespace DigitalLibrary.Web.ViewModels.Work
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class WorkPublicCreateViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Use 5-100 characters")]
        [UIHint("SingleLineText")]
        public string Title { get; set; }

        [StringLength(1000, MinimumLength = 5, ErrorMessage = "Use 5-1000 characters")]
        [UIHint("MultiLineText")]
        public string Description { get; set; }

        [Display(Name = "Year")]
        [UIHint("DropDownList")]
        public int Year { get; set; }

        [Display(Name = "Author")]
        [UIHint("DropDownList")]
        public int AuthorId { get; set; }

        [Display(Name = "Genre")]
        [UIHint("DropDownList")]
        public int GenreId { get; set; }

        public IEnumerable<SelectListItem> Years { get; set; }

        public IEnumerable<SelectListItem> Authors { get; set; }

        public IEnumerable<SelectListItem> Genres { get; set; }
    }
}