using SurveySheet.Repositories.Models;

namespace SurveySheet.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string username);
    }
}
