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
        public async Task<ApiResponse<string>> AddStateNumber(AddStateNumberDto request)
        {
            var stateNumberDB = await _unitOfWor.StateNumber
                .Where(x => x.Number == request.StateNumber.ToUpper()).FirstOrDefaultAsync();

            if (stateNumberDB != null) return new BadApiResponse<string>("This Number Is Alredy Exist");

            var creatStateNumber = new StateNumber
            {
                Number = request.StateNumber.ToUpper(),
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

        public async Task<ApiResponse<List<GetStateNumberDto>>> GetNumberByFiltering(FilteringStateNumberDto request)
        {
            var data = _unitOfWor.StateNumber.AsQuareble();

            if (request.Id != null)
                data = data.Where(x => x.Id == request.Id);
            if (!string.IsNullOrEmpty(request.StateNumber))
                data = data.Where(x => x.Number.Contains(request.StateNumber.ToUpper()));
            if (request.CreateFrom != null)
                data = data.Where(x => x.CreateTime >= request.CreateFrom);
            if (request.CreatoTo != null)
                data = data.Where(x => x.CreateTime <= request.CreatoTo);

            data = data.Skip((request.PageNumb - 1) * request.PageSize).Take(request.PageSize);
            var result = await data.ToListAsync();

            return new SuccessApiResponse<List<GetStateNumberDto>>(result.Select(x =>
            {
                return new GetStateNumberDto
                {
                    Id = x.Id,
                    CreateTime = x.CreateTime,
                    StateNumber = x.Number
                };
            }).ToList());
        }

        public async Task<ApiResponse<NumberDetailInfoDto>> GetNumberDetailInfoById(int numberId)
        {
            var numberDetailsDb = await _unitOfWor.StateNumber
                .Where(x => x.Id == numberId)
                .Include(x => x.StateNumberOrder)
                .Include(x => x.StateNumberReservation)
                .FirstOrDefaultAsync();

            if (numberDetailsDb == null) return new BadApiResponse<NumberDetailInfoDto>("The number does not exist");
            var result = new NumberDetailInfoDto
            {
                Id=numberDetailsDb.Id,
                StateNumber = numberDetailsDb.Number,
                CreateTime = numberDetailsDb.CreateTime,

                StateNumberOrder = numberDetailsDb.StateNumberOrder.Select(x =>
                {
                    return new StateNumberOrderDto
                    {
                        Id = x.Id,
                        StateNumberId= numberDetailsDb.Id,
                        CreateTime = x.CreateTime,


                    };
                }).FirstOrDefault(),
                StateNumberReservation = numberDetailsDb.StateNumberReservation.Select(x =>
                {
                    return new StateNumberReservationDto
                    {
                        Id = x.Id,
                        EndReservation = x.EndReservation,
                        CreateTime = x.CreateTime,
                        StateNumberId= numberDetailsDb.Id
                    };
                }).FirstOrDefault()
            };
            return new SuccessApiResponse<NumberDetailInfoDto>(result);
        }

        public async Task<ApiResponse<string>>ReservationNumber(AddStateNumberDto request)
        {
            var numberDb = await _unitOfWor.StateNumber
                .Where(x => x.Number == request.StateNumber.ToUpper())
                .Include(x => x.StateNumberReservation).FirstOrDefaultAsync();

            if (numberDb == null)
                return new BadApiResponse<string>("The number does not exist");
            if (numberDb.StateNumberOrder.Count!=0)
                return new BadApiResponse<string>("The number is alredy reserved");

            var numberReservation = new StateNumberReservation
            {
                StateNumberId = numberDb.Id,
                CreateTime = DateTime.Now,
                EndReservation = DateTime.Now.AddMonths(2)
            };
            await _unitOfWor.StateNumberReservation.AddAsync(numberReservation);
            await _unitOfWor.SaveChangesAsync();
            return new SuccessApiResponse<string>("The number has been reserved succesfully");
        }

        public async Task<ApiResponse<string>> OrderNumber(AddStateNumberDto request)
        {
            var numberDb = await _unitOfWor.StateNumber
                .Where(x => x.Number == request.StateNumber.ToUpper())
                .Include(x => x.StateNumberReservation)
                .Include(x=>x.StateNumberOrder)
                .FirstOrDefaultAsync();

            if (numberDb == null)
                return new BadApiResponse<string>("The number does not exist");
            if (numberDb.StateNumberOrder.Count!=0)
                return new BadApiResponse<string>("The number is alredy Ordered");


            var numberOrder = new StateNumberOrder
            {
                StateNumberId= numberDb.Id,
                CreateTime= DateTime.Now
            };
            await _unitOfWor.StateNumberOrder.AddAsync(numberOrder);
            await _unitOfWor.SaveChangesAsync();
            return new SuccessApiResponse<string>("The number has been reserved succesfully");
        }
    }
}
