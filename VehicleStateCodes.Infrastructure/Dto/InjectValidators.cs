using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleStateCodes.Infrastructure.Dto.StateNumberDto;
using VehicleStateCodes.Infrastructure.Dto.UserDto;

namespace VehicleStateCodes.Infrastructure.Dto
{
    public static class InjectValidators
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UserLogInDto>, UserLogInDtoValidator>();
            services.AddScoped<IValidator<UserUpdateDto>, UserUpdateDtoValidator>();
            services.AddScoped<IValidator<ChangePasswordDto>, ChangePasswordDtoValidator>();
            services.AddScoped<IValidator<UserRegistrationDto>, UserRegistrationDtoValidator>();
            services.AddScoped<IValidator<AddStateNumberDto>, AddStateNumberDtoValidator>();
            services.AddScoped<IValidator<UpdateStateNumberDto>, UpdateStateNumberDtoValidator>();
            services.AddScoped<IValidator<FilteringStateNumberDto>, FilteringStateNumberDtoValidator>();
            return services;

            


        }
    }
}
