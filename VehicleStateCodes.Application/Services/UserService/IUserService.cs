using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleStateCodes.Infrastructure.ApiServiceResponse;
using VehicleStateCodes.Infrastructure.Dto.UserDto;

namespace VehicleStateCodes.Application.Services.UserService
{
    public interface IUserService
    {
        Task<ApiResponse<string>> Registration(UserRegistrationDto request);
        Task<ApiResponse<string>> LogIn(UserLogInDto request);
        Task<ApiResponse<UserUpdateDto>> Update(int id, UserUpdateDto request);
        Task<ApiResponse<string>> UpdatePassword(ChangePasswordDto request, int id);
        Task<ApiResponse<string>> Delete(int id);
    }
}
