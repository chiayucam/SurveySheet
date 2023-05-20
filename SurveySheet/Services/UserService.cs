using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SurveySheet.Enums;
using SurveySheet.Repositories.Interfaces;
using SurveySheet.Repositories.Models;
using SurveySheet.Services.Interfaces;
using SurveySheet.Services.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveySheet.Services
{
    public class UserService : IUserService
    {
        private IConfiguration Config;

        private IUserRepository UserRepository;

        private PasswordHasher<string> PasswordHasher = new PasswordHasher<string>();

        public UserService(IConfiguration config, IUserRepository userRepository)
        {
            Config = config;
            UserRepository = userRepository;
        }

        public async Task<UserRoleDto> AuthenticateUserAsync(UserDto userDto)
        {
            var user = await UserRepository.GetUserAsync(userDto.Username);

            var verifyResult = PasswordHasher.VerifyHashedPassword(userDto.Username, user.PasswordHash, userDto.Password);

            if (verifyResult == PasswordVerificationResult.Failed)
            {
                throw new InvalidOperationException("Username or passward failed");
            }

            return new UserRoleDto (userDto, user.Role);
        }

        public string GenerateToken(Role role = Role.User)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, role.ToString())
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:key"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Config["Jwt:Issuer"],
                Config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
