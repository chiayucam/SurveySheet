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

        /// <summary>
        /// 管理員、用戶登入
        /// </summary>
        /// <param name="request">登入資訊</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            try
            {
                var userDto = new UserDto() { Username = request.UserName, Password = request.Password };
                var userRoleDto = await UserService.AuthenticateUserAsync(userDto);
                var token = UserService.GenerateToken(userRoleDto);

                var response = new LoginResponse() { Token = token };
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 管理員、用戶註冊
        /// </summary>
        /// <param name="request">註冊資訊</param>
        /// <remarks>
        /// Roles:
        /// - 0 Admin
        /// - 1 User
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("SignUp")]
        public async Task<ActionResult> SignUp(SignUpRequest request)
        {
            try
            {
                var userDto = new UserDto() { Username = request.UserName, Password = request.Password };
                var userRoleDto = new UserRoleDto(userDto, request.Role);
                await UserService.CreateUserAsync(userRoleDto);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
