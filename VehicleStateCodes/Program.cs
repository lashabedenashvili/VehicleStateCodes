using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using VehicleStateCodes;
using VehicleStateCodes.Data.Domein.Domein;
using VehicleStateCodes.DataBase.UnitOfWork;

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

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddScoped<IUnitOfWork, UnitOFWork>();
    builder.Services.AddSwaggerGen();

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