
using VehicleStateCodes.Infrastructure.ApiServiceResponse;
using VehicleStateCodes.Infrastructure.Dto;
using VehicleStateCodes.Infrastructure.Dto.StateNumberDto;

namespace VehicleStateCodes.Application.Services.StateNumberServ
{
    public interface IStateNumberService
    {
        Task<ApiResponse<string>> AddStateNumber(string stateNumber);
        Task<ApiResponse<UpdateStateNumberDto>> UpdateStateNumber(UpdateStateNumberDto request,int id);
        Task<ApiResponse<string>> DeleteNumber(int stateNumberId);
        Task<ApiResponse<List<GetStateNumberDto>>> GetNumberByFiltering(FilteringStateNumberDto request);
    }
}
