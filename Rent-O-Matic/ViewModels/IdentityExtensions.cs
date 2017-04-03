using System.Security.Claims;
using System.Security.Principal;

namespace Rent_O_Matic.ViewModels
{
    public static class IdentityExtensions
    {
        public static string GetPersonsName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Name");

            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}