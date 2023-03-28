using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleStateCodes.Application.Services.StateNumberServ;
using VehicleStateCodes.Infrastructure.ApiServiceResponse;
using VehicleStateCodes.Infrastructure.Dto;
using VehicleStateCodes.Infrastructure.Dto.StateNumberDto;

namespace VehicleStateCodes.Controllers
{
    public class StateNumberController:BaseController
    {
        private readonly IStateNumberService _stateNumberService;

        public StateNumberController(IStateNumberService stateNumberService )
        {
            _stateNumberService = stateNumberService;
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("AddStateNumber")]
        public async Task<ActionResult<ApiResponse<string>>> AddStateNumber(AddStateNumberDto request)
        {
            return ResponseResult(await _stateNumberService.AddStateNumber(request));
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("UpdateStateNumber")]
        public async Task<ActionResult<ApiResponse<UpdateStateNumberDto>>> UpdateStateNumber(UpdateStateNumberDto request, int id)
        {
            return ResponseResult(await _stateNumberService.UpdateStateNumber(request, id));
        }



        [HttpPost("GetNumberByFilterin")]
        public async Task<ActionResult<ApiResponse<List<GetStateNumberDto>>>> GetNumberByFilterin(FilteringStateNumberDto request)
        {
            return ResponseResult(await _stateNumberService.GetNumberByFiltering(request));
        }

        [HttpPost("GetDetailInfoById")]
        public async Task<ActionResult<ApiResponse<NumberDetailInfoDto>>> GetDetailInfoById(int numberId)
        {
            return ResponseResult(await _stateNumberService.GetNumberDetailInfoById(numberId));
        }
        [Authorize(Roles="admin",AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("DeleteNumber")]
        public async Task<ActionResult<ApiResponse<string>>>DeleteNumber(int numberId)
        {
            return ResponseResult(await _stateNumberService.DeleteNumber(numberId));
        }

        [HttpPost("ReservationNumber")]

        public async Task<ActionResult<ApiResponse<string>>> ReservationNumber(AddStateNumberDto request)
        {
            return ResponseResult(await _stateNumberService.ReservationNumber(request));
        }


        [HttpPost("OrderNumber")]

        public async Task<ActionResult<ApiResponse<string>>> OrderNumber(AddStateNumberDto request)
        {
            return ResponseResult(await _stateNumberService.OrderNumber(request));
        }

    }
}
