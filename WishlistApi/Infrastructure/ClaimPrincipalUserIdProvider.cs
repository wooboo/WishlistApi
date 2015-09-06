using System.Security.Claims;
using System.Threading;

namespace WishlistApi.Infrastructure
{
    public class ClaimPrincipalUserIdProvider:IUserIdProvider 
    {
        public string GetUserId()
        {
            var caller = Thread.CurrentPrincipal as ClaimsPrincipal;

            var subjectClaim = caller.FindFirst("sub");

            var username = subjectClaim.Value;
            return username;

        }
    }

    public class FakeUserIdProvider : IUserIdProvider
    {
        public string GetUserId()
        {
            return "ThisIsTheUser";
        }
    }
}