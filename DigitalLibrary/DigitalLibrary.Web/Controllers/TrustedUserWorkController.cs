namespace DigitalLibrary.Web.Controllers
{
    using System.Collections;
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.UI;

    using DigitalLibrary.Data;
    using DigitalLibrary.Data.Logic;
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Areas.Administration.Controllers.Base;
    using DigitalLibrary.Web.ViewModels.Work;

    // [Authorize(Roles = "trusted")]
    public class TrustedUserWorkController : KendoGridAdministrationController
    {
        private const int TrustedRoleNeededRating = 70;
        private const int MinimumPositiveUploadsToBecomeTrusted = 3;

        public TrustedUserWorkController(IDigitalLibraryData data)
            : base(data)
        {
        }

        public ActionResult List()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, WorkPublicListViewModel model)
        {
            var workToBeApprooved = this.GetById<Work>(model.Id);
            var uploadedBy = workToBeApprooved.UploadedBy;

            uploadedBy.PositiveUploads++;

            workToBeApprooved.IsApproved = true;

            this.Data.SaveChanges();

            if (uploadedBy.Rating > TrustedRoleNeededRating && uploadedBy.PositiveUploads >= MinimumPositiveUploadsToBecomeTrusted)
            {
                this.IdentityManager.AddUserToRole(uploadedBy.Id, "trusted");
            }

            return this.GridOperation(model, request);
        }

        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, WorkPublicListViewModel model)
        {
            var workToBeDeleted = this.GetById<Work>(model.Id);
            var uploadedBy = workToBeDeleted.UploadedBy;

            uploadedBy.NegativeUploads++;

            base.Destroy<Work>(model.Id);

            FileManager.DeleteFile(workToBeDeleted.ZipFileLink);

            if (uploadedBy.Rating < TrustedRoleNeededRating)
            {
                this.IdentityManager.ClearUserRoles(uploadedBy.Id, "trusted");
            }

            return this.GridOperation(model, request);
        }

        protected override IEnumerable GetData()
        {
            var unApprovedWorks = this.Data.Works
                .All()
                .Where(w => !w.IsApproved)
                .Select(WorkPublicListViewModel.FromWork);

            return unApprovedWorks;
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Works.GetById(id) as T;
        }
    }
}