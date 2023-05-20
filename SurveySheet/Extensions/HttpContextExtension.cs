using System.Security.Claims;

namespace SurveySheet.Extensions
{
    public static class HttpContextExtension
    {
        public static int? GetUserId(this HttpContext context)
        {
            if (context.User == null)
            {
                return null;
            }

            return int.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
    }
}
