namespace DigitalLibrary.Web.Controllers
{
    using System.Collections;
    using System.Web.Mvc;

    using DigitalLibrary.Data;
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Areas.Administration.Controllers.Base;
    using DigitalLibrary.Web.Infrastructure.Services.Contracts;
    using DigitalLibrary.Web.ViewModels.Work;

    using Kendo.Mvc.UI;

    [Authorize(Roles = "trusted")]
    public class TrustedUserWorkController : KendoGridCRUDController
    {
        private ITrustedUserService trustedUserServices;

        public TrustedUserWorkController(IDigitalLibraryData data, ITrustedUserService trustedUserServices)
            : base(data)
        {
            this.trustedUserServices = trustedUserServices;
        }

        public ActionResult List()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, WorkPublicListViewModel model)
        {
            var workToBeApprooved = this.GetById<Work>(model.Id);
            this.trustedUserServices.Approove(workToBeApprooved);

            this.Data.SaveChanges();
            return this.GridOperation(model, request);
        }

        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, WorkPublicListViewModel model)
        {
            var workToBeDeleted = this.GetById<Work>(model.Id);
            this.trustedUserServices.Destroy(workToBeDeleted);
            base.Destroy<Work>(workToBeDeleted.Id);

            return this.GridOperation(model, request);
        }

        protected override IEnumerable GetData()
        {
            return this.trustedUserServices.GetUnApprovedWorks();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Works.GetById(id) as T;
        }
    }
}