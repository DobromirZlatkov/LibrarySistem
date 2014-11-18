namespace DigitalLibrary.Web.Infrastructure.Services.Contracts
{
    using System.Linq;

    using DigitalLibrary.Models;
    using DigitalLibrary.Web.ViewModels.Work;

    public interface ITrustedUserService
    {
        void Destroy(Work workToBeDestroyed);

        void Approove(Work workToBeApproved);

        IQueryable<WorkPublicListViewModel> GetUnApprovedWorks();
    }
}