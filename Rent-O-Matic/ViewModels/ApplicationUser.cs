using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Rent_O_Matic.ViewModels
{
    public class ApplicationUser : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("Name", Name.ToString()));
            userIdentity.AddClaim(new Claim("Nationality", Nationality.ToString()));
            userIdentity.AddClaim(new Claim("YearsOld", YearsOld.ToString()));
            userIdentity.AddClaim(new Claim("DrivingLiscense", DrivingLiscense.ToString()));
            return userIdentity;
        }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Nationality { get; set; }

        [Required]
        public int YearsOld { get; set; }

        [Required]
        [StringLength(100)]
        public string DrivingLiscense { get; set; }

    }
}