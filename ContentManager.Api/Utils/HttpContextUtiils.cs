using System.Security.Claims;

namespace ContentManager.Api.Utils
{
    public static class HttpContextUtiils
    {
        public static Guid UserId(this HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            var userId = identity!.Claims.FirstOrDefault(x => x.Type.Equals("userId"))!.Value;

            return Guid.Parse(userId.Replace("-", string.Empty));
        }
    }
}
