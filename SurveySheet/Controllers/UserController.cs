using Microsoft.AspNetCore.Mvc;
using SurveySheet.Controllers.Requests;
using SurveySheet.Controllers.Responses;
using SurveySheet.Services.Interfaces;
using SurveySheet.Services.Models;

namespace SurveySheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService UserService;

        public UserController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            try
            {
                var userDto = new UserDto() { Username = request.UserName, Password = request.Password };
                var userRoleDto = await UserService.AuthenticateUserAsync(userDto);
                var token = UserService.GenerateToken(userRoleDto.Role);

                var response = new LoginResponse() { Token = token };
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
