using DigitalLibrary.Data;
using DigitalLibrary.Data.Logic;
using DigitalLibrary.Web.ViewModels;
using DigitalLibrary.Web.ViewModels.Work;
using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalLibrary.Web.Controllers
{


    // [Authorize(Roles = "trusted")]
    public class TrustedUserWorkController : BaseController
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


        public IQueryable<WorkPublicListViewModel> GetAllUnApprovedWorks()
        {
            var unApprovedWorks = this.Data.Works
                .All()
                .Where(w => !w.IsApproved)
                .Select(WorkPublicListViewModel.FromWork);

            return unApprovedWorks;
        }

        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var data = this.GetAllUnApprovedWorks()
                .ToDataSourceResult(request);

            return this.Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Approove(int id)
        {

            var workToBeApprooved = this.Data.Works.GetById(id);
            var uploadedBy = workToBeApprooved.UploadedBy;

            uploadedBy.PositiveUploads++;

            workToBeApprooved.IsApproved = true;

            this.Data.SaveChanges();

            if (uploadedBy.Rating > TrustedRoleNeededRating && uploadedBy.PositiveUploads >= MinimumPositiveUploadsToBecomeTrusted)
            {
                this.IdentityManager.AddUserToRole(uploadedBy.Id, "trusted");
            }

            Response.Redirect("~/TrustedUserWork/List");
            return this.View("List");
        }

        public ActionResult Delete([DataSourceRequest]DataSourceRequest request, WorkPublicListViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var workToBeDeleted = this.Data.Works.GetById(model.Id);

                var uploadedBy = workToBeDeleted.UploadedBy;
                uploadedBy.NegativeUploads++;

                this.Data.Works.Delete(workToBeDeleted);
                this.Data.SaveChanges();

                var folders = workToBeDeleted.ZipFileLink.Split('/').ToList();

                folders.RemoveAt(folders.Count - 1);

                var filePath = string.Join("/",folders);

                FileManager.DeleteFile(filePath);

                if (uploadedBy.Rating < TrustedRoleNeededRating)
                {
                    this.IdentityManager.ClearUserRoles(uploadedBy.Id, "trusted");
                }
            }

            return this.Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }
    }
}