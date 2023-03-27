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
        [HttpPost("AddStateNumber")]
        public async Task<ActionResult<ApiResponse<string>>> AddStateNumber(string stateNumber)
        {
            return ResponseResult(await _stateNumberService.AddStateNumber(stateNumber));
        }

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
    }
}
