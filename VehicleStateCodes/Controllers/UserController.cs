using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("UserUpdate")]
        public async Task<ActionResult<ApiResponse<UserUpdateDto>>> UserUpdate(UserUpdateDto request)
        {
            var id = GetId();
            return ResponseResult(await _userService.Update(id,request));
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("PasswordUpdate")]
        public async Task<ActionResult<ApiResponse<string>>> UserUPasswordUpdatepdate( ChangePasswordDto request)
        {
            var id = GetId();
            return ResponseResult(await _userService.UpdatePassword( request, id));
        }

        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("DeleteUser")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteUser(int id)
        {            
            return ResponseResult(await _userService.Delete(id));
        }

    }
}
