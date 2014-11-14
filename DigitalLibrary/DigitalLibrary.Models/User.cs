namespace DigitalLibrary.Models
{

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using DigitalLibrary.Data.Logic;

    public class User : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public int PositiveUploads { get; set; }

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
