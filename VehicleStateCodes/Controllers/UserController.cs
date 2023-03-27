using Microsoft.AspNetCore.Mvc;
using VehicleStateCodes.Application.Services.UserService;
using VehicleStateCodes.Infrastructure.ApiServiceResponse;
using VehicleStateCodes.Infrastructure.Dto.UserDto;

namespace VehicleStateCodes.Controllers
{
    public class UserController:BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("UserRegistration")]
        public async Task<ActionResult<ApiResponse<string>>> UserRegistration(UserRegistrationDto request)
        {
            return ResponseResult(await _userService.Registration(request));
        }

        [HttpPost("UserLogIn")]
        public async Task<ActionResult<ApiResponse<string>>> UserLogIn(UserLogInDto request)
        {
            return ResponseResult(await _userService.LogIn(request));
        }

        [HttpPost("UserUpdate")]
        public async Task<ActionResult<ApiResponse<UserUpdateDto>>> UserUpdate(int id,UserUpdateDto request)
        {
            return ResponseResult(await _userService.Update(id,request));
        }

        [HttpPost("PasswordUpdate")]
        public async Task<ActionResult<ApiResponse<string>>> UserUPasswordUpdatepdate(int id, ChangePasswordDto request)
        {
            return ResponseResult(await _userService.UpdatePassword( request, id));
        }
    }
}
