namespace DigitalLibrary.Web.Infrastructure.Services
{
    using System.Linq;

    using DigitalLibrary.Data;
    using DigitalLibrary.Data.Logic;
    using DigitalLibrary.Models;
    using DigitalLibrary.Web.Controllers;
    using DigitalLibrary.Web.Infrastructure.Services.Contracts;
    using DigitalLibrary.Web.ViewModels.Work;

    public class TrustedUserService : BaseController, ITrustedUserService
    {
        private const int TrustedRoleNeededRating = 70;
        private const int MinimumPositiveUploadsToBecomeTrusted = 3;

        public TrustedUserService(IDigitalLibraryData data)
        : base(data)
        {
        }

        public void Approove(Work workToBeApprooved)
        {
            var uploadedBy = workToBeApprooved.UploadedBy;

            uploadedBy.PositiveUploads++;

            workToBeApprooved.IsApproved = true;

            this.Data.SaveChanges();

            if (uploadedBy.Rating > TrustedRoleNeededRating && uploadedBy.PositiveUploads >= MinimumPositiveUploadsToBecomeTrusted)
            {
                this.IdentityManager.AddUserToRole(uploadedBy.Id, "trusted");
            }
        }

        public void Destroy(Work workToBeDestroyed)
        {
            var uploadedBy = workToBeDestroyed.UploadedBy;

            uploadedBy.NegativeUploads++;

            FileManager.DeleteFile(workToBeDestroyed.ZipFileLink);

            if (uploadedBy.Rating < TrustedRoleNeededRating)
            {
                this.IdentityManager.ClearUserRoles(uploadedBy.Id, "trusted");
            }
        }

        public IQueryable<WorkPublicListViewModel> GetUnApprovedWorks()
        {
            var unApprovedWorks = this.Data.Works
              .All()
              .Where(w => !w.IsApproved)
              .Select(WorkPublicListViewModel.FromWork);

            return unApprovedWorks;
        }
    }
}