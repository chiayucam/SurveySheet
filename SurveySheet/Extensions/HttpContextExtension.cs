using System.Security.Claims;

namespace SurveySheet.Extensions
{
    public static class HttpContextExtension
    {
        public static int? GetUserId(this HttpContext context)
        {
            if (context.User.Identity!.IsAuthenticated == false)
            {
                return null;
            }

            return int.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}
