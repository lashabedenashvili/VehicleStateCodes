using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleStateCodes.Data.Domein.Data;
using VehicleStateCodes.DataBase.UnitOfWork;
using VehicleStateCodes.Infrastructure.ApiServiceResponse;
using VehicleStateCodes.Infrastructure.Dto;
using VehicleStateCodes.Infrastructure.Dto.StateNumberDto;

namespace VehicleStateCodes.Application.Services.StateNumberServ
{

    public class StateNumberService : IStateNumberService
    {
        private readonly IUnitOfWork _unitOfWor;

        public StateNumberService(IUnitOfWork unitOfWor)
        {
            _unitOfWor = unitOfWor;
        }
        public async Task<ApiResponse<string>> AddStateNumber(string stateNumber)
        {
            var stateNumberDB = await _unitOfWor.StateNumber
                .Where(x => x.Number == stateNumber.ToUpper()).FirstOrDefaultAsync();

            if (stateNumberDB != null) return new BadApiResponse<string>("This Number Is Alredy Exist");

            var creatStateNumber = new StateNumber
            {
                Number = stateNumber.ToUpper(),
                CreateTime = DateTime.Now,
            };
            await _unitOfWor.StateNumber.AddAsync(creatStateNumber);
            await _unitOfWor.SaveChangesAsync();
            return new SuccessApiResponse<string>("The number has been added successfully");
        }

        public async Task<ApiResponse<UpdateStateNumberDto>> UpdateStateNumber(UpdateStateNumberDto request, int id)
        {
            var stateNumberDb = await _unitOfWor.StateNumber
                .Where(x => x.Id == id).FirstOrDefaultAsync();
            if (stateNumberDb == null) return new BadApiResponse<UpdateStateNumberDto>("This State number does not exist");

            stateNumberDb.Number = request.StateNumber != null ? request.StateNumber.ToUpper() : stateNumberDb.Number;
            stateNumberDb.CreateTime = request.CreateTime != null ? request.CreateTime : stateNumberDb.CreateTime;
            await _unitOfWor.StateNumber.Update(stateNumberDb);
            await _unitOfWor.SaveChangesAsync();

            return new SuccessApiResponse<UpdateStateNumberDto>(new UpdateStateNumberDto
            {
                StateNumber = stateNumberDb.Number,
                CreateTime = stateNumberDb.CreateTime
            });
        }

        public async Task<ApiResponse<string>> DeleteNumber(int stateNumberId)
        {
            var stateNumberDb = await _unitOfWor.StateNumber
                .Where(x => x.Id == stateNumberId).FirstOrDefaultAsync();

            if (stateNumberDb == null) return new BadApiResponse<string>("This number does not exist");
            await _unitOfWor.StateNumber.Delete(stateNumberDb);
            await _unitOfWor.SaveChangesAsync();
            return new SuccessApiResponse<string>("The number has been deleted");
        }

        //public async Task<ApiResponse<List<NumberDetailInfoDto>>> GetNumberByFiltering(FilteringStateNumberDto request)
        //{
        //    var data = _unitOfWor.StateNumber
        //        .AsQuareble()
        //        .Include(x => x.StateNumberOrder)
        //        .Include(x => x.StateNumberReservation).AsQueryable();

        //    if (request.Id != null)
        //        data = data.Where(x => x.Id == request.Id);
        //    if (!string.IsNullOrEmpty(request.StateNumber))
        //        data = data.Where(x => x.Number.Contains(request.StateNumber.ToUpper()));
        //    if (request.CreateFrom != null)
        //        data = data.Where(x => x.CreateTime >= request.CreateFrom);
        //    if (request.CreatoTo != null)
        //        data = data.Where(x => x.CreateTime <= request.CreatoTo);

        //    data = data.Skip((request.PageNumb - 1) * request.PageSize).Take(request.PageSize);
        //    var result = await data.ToListAsync();

        //    return 
            
           
        //}

    }
}
