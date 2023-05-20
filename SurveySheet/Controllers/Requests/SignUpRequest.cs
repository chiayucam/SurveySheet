using SurveySheet.Enums;

namespace SurveySheet.Controllers.Requests
{
    public class SignUpRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public Role Role { get; set; }
    }
}
