using SurveySheet.Enums;

namespace SurveySheet.Repositories.Models
{
    public class User
    {
        public int Id { get; set; }

        public Role Role { get; set; }

        public string Username { get; set; }

        public string PasswordHash {get; set;}
    }
}
