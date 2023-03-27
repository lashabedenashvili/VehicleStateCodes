using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VehicleStateCodes.Infrastructure.ApiServiceResponse;

namespace VehicleStateCodes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        protected ActionResult<ApiResponse<T>> ResponseResult<T>(ApiResponse<T> apiResponse)
        {
            switch (apiResponse.statusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return Ok(apiResponse);
                case System.Net.HttpStatusCode.BadRequest:
                    return BadRequest(apiResponse);
                default:
                    return Ok(apiResponse);
            }
        }
        protected int GetId()
        {
            return Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }
        protected string GetEmail()
        {
            return User.FindFirstValue(ClaimTypes.Email);
        }
    }
}
