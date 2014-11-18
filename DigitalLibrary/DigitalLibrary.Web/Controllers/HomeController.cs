namespace DigitalLibrary.Web.Controllers
{
    using System.Web.Caching;
    using System.Web.Mvc;

    using DigitalLibrary.Data;
    using DigitalLibrary.Web.Infrastructure.Services.Contracts;

    public class HomeController : BaseController
    {
        private IHomeServices homeServices;

        public HomeController(IDigitalLibraryData data, IHomeServices homeServices)
            : base(data)
        {
            this.homeServices = homeServices;
        }

        //[OutputCache(Duration = 60 * 60)]
        public ActionResult Index()
        {
           return this.View();
        }

        public ActionResult Error()
        {
            return this.View();
        }

        //[OutputCache(Duration = 60 * 60)]
        public ActionResult TopUsers()
        {
            return this.PartialView("_TopRatedUsers", this.homeServices.GetTopUsers(6));
        }

        //[OutputCache(Duration = 60 * 60)]
        public ActionResult Statistics()
        {
            return this.PartialView("_Statistics", this.homeServices.GetStatistics());
        }

        //[OutputCache(Duration = 60 * 60)]
        public ActionResult TopWorks()
        {
            return this.PartialView("_TopRatedWorks", this.homeServices.GetTopRatedWorks(6));
        }

        //[OutputCache(Duration = 60 * 60)]
        public ActionResult Genres()
        {
            return this.PartialView("_GenresAndWorks", this.homeServices.AllGenres());
        }
    }
}