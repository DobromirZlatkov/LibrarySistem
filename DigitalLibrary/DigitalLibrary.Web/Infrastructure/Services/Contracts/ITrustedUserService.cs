namespace DigitalLibrary.Web.Infrastructure.Services.Contracts
{
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.UI;

    using DigitalLibrary.Web.ViewModels.Work;
    using DigitalLibrary.Models;

    public interface ITrustedUserService
    {

        void Destroy(Work workToBeDestroyed);

        void Approove(Work workToBeApproved);

        IQueryable<WorkPublicListViewModel> GetUnApprovedWorks();
    }
}