using FluentValidation.AspNetCore;
using Library.Infrastructure.PropertyValidator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using System.Reflection;
using VehicleStateCodes;
using VehicleStateCodes.Application.Services.StateNumberServ;
using VehicleStateCodes.Application.Services.UserService;
using VehicleStateCodes.Data.Domein.Domein;
using VehicleStateCodes.DataBase.GenericRepository;
using VehicleStateCodes.DataBase.Repositories.Implementation;
using VehicleStateCodes.DataBase.Repositories.Interface;
using VehicleStateCodes.DataBase.UnitOfWork;
using VehicleStateCodes.Infrastructure.Dto;

var logger = NLog.LogManager
    .Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
try
{

    var builder = WebApplication.CreateBuilder(args);

    // log youe application at trace level 
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

    // Register the NLog service
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // Add services to the container.
    builder.Services.AddDbContext<Context>(options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    //Fluent Validator
    builder.Services.AddControllers().AddFluentValidation(options =>
    {
        options.ImplicitlyValidateChildProperties = true;
        options.ImplicitlyValidateRootCollectionElements = true;

        options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    });

    //validator
    builder.Services.AddValidators();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddScoped<IUnitOfWork, UnitOFWork>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IStateNumberService, StateNumberService>();
    builder.Services.AddScoped<IPropertyValidators, PropertyValidators>();
    builder.Services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepository<>));
    builder.Services.AddSwaggerGen();

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer("Bearer", options =>
       {
           options.TokenValidationParameters = new TokenValidationParameters
           {

               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
               .GetBytes(builder.Configuration
               .GetSection("AppSettings:Token").Value)),
               ValidateIssuer = false,
               ValidateAudience = false,
           };
       });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{

    logger.Error(ex);
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}