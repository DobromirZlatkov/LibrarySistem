namespace DigitalLibrary.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Caching;
    using System.Web.Mvc;
    using System.Collections;

    using DigitalLibrary.Data;
    using DigitalLibrary.Web.ViewModels.Users;
    using DigitalLibrary.Web.ViewModels.Genre;
    using DigitalLibrary.Web.ViewModels.Work;
    using DigitalLibrary.Web.ViewModels.Home;
    using DigitalLibrary.Web.Infrastructure.Services.Contracts;


    public class HomeController : BaseController
    {
        private IHomeServices homeServices;

        public HomeController(IDigitalLibraryData data, IHomeServices homeServices)
            : base(data)
        {
            this.homeServices = homeServices;
        }

        [OutputCache(Duration = 60 * 60)]
        public ActionResult Index()
        {
           return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        //[OutputCache(Duration = 60 * 60)]
        public ActionResult TopUsers()
        {
            return PartialView("_TopRatedUsers", this.homeServices.GetTopUsers(6));
        }

       // [OutputCache(Duration = 60 * 60)]
        public ActionResult Statistics()
        {
            return PartialView("_Statistics", this.homeServices.GetStatistics());
        }

      //  [OutputCache(Duration = 60 * 60)]
        public ActionResult TopWorks()
        {
            return PartialView("_TopRatedWorks", this.homeServices.GetTopRatedWorks(6));
        }

     //  [OutputCache(Duration = 60 * 60)]
        public ActionResult Genres()
        {
            return PartialView("_GenresAndWorks", this.homeServices.AllGenres());
        }
    }
}