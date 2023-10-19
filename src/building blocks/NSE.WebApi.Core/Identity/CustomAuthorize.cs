using Microsoft.AspNetCore.Http;

namespace NSE.WebApi.Core.Identity
{
    public class CustomAuthorize
    {
        public static bool CheckUserClaims(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated &&
                context.User.Claims.Any(c => c.Type == claimName && c.Value.Contains(claimValue));
        }
    }
}
