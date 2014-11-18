namespace DigitalLibrary.Models
{
    using System.ComponentModel;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using DigitalLibrary.Data.Logic;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        [DefaultValue(1)]
        public int PositiveUploads { get; set; }

        [DefaultValue(1)]
        public int NegativeUploads { get; set; }

        public double Rating
        {
            get
            {
                return PercentageCalculator.CalculatePersentage(this.PositiveUploads, this.NegativeUploads);
            }
        }
    }
}
