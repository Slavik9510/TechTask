using TechTask.Data;
using TechTask.Data.Settings;
using TechTask.Interfaces;
using TechTask.Mapping;
using TechTask.Services;

namespace TechTask.Extensions;

public static class ApplicationServiceExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services,
			IConfiguration config)
	{
		services.Configure<MongoDBSettings>(config.GetSection("MongoDB"));
		services.AddSingleton<MongoDBContext>();

		services.AddAutoMapper(typeof(AutoMapperProfiles));
		services.AddScoped<ICustomersRepository, MongoCustomersRepository>();
		services.AddScoped<ICustomersService, CustomersService>();

		return services;
	}
}
