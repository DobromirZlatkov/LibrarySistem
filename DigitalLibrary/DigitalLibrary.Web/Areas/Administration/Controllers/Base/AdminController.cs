namespace DigitalLibrary.Web.Areas.Administration.Controllers.Base
{
    using DigitalLibrary.Data;
    using DigitalLibrary.Web.Controllers;
    // [Authorize(Roles = "Admin")]
    public abstract class AdminController : BaseController
    {
        public AdminController(IDigitalLibraryData data)
            : base(data)
        {

        }
    }
}