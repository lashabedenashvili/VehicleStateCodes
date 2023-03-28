
using VehicleStateCodes.Infrastructure.ApiServiceResponse;
using VehicleStateCodes.Infrastructure.Dto;
using VehicleStateCodes.Infrastructure.Dto.StateNumberDto;

namespace VehicleStateCodes.Application.Services.StateNumberServ
{
    public interface IStateNumberService
    {
        Task<ApiResponse<string>> AddStateNumber(AddStateNumberDto request);
        Task<ApiResponse<UpdateStateNumberDto>> UpdateStateNumber(UpdateStateNumberDto request,int id);
        Task<ApiResponse<string>> DeleteNumber(int stateNumberId);
        Task<ApiResponse<List<GetStateNumberDto>>> GetNumberByFiltering(FilteringStateNumberDto request);
        Task<ApiResponse<NumberDetailInfoDto>> GetNumberDetailInfoById(int numberId);
        Task<ApiResponse<string>> ReservationNumber(AddStateNumberDto request);
        Task<ApiResponse<string>> OrderNumber(AddStateNumberDto request);
    }
}
