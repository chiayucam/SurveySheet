using SurveySheet.Enums;

namespace SurveySheet.Services.Models
{
    public class UserRoleDto : UserDto
    {
        public UserRoleDto(UserDto userDto, Role role, int id = 0)
        {
            Username = userDto.Username;
            Password = userDto.Password;
            Role = role;
            Id = id;
        }

        public int Id { get; set; }

        public Role Role { get; set; }
    }
}
