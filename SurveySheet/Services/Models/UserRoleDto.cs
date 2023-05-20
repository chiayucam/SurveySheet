using SurveySheet.Enums;

namespace SurveySheet.Services.Models
{
    public class UserRoleDto : UserDto
    {
        public UserRoleDto(UserDto userDto, Role role)
        {
            Username = userDto.Username;
            Password = userDto.Password;
            Role = role;
        }

        public Role Role { get; set; }
    }
}
