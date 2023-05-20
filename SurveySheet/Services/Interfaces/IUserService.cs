using SurveySheet.Enums;
using SurveySheet.Services.Models;

namespace SurveySheet.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserRoleDto> AuthenticateUserAsync(UserDto userDto);

        string GenerateToken(Role role);
    }
}
