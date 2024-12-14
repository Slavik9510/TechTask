using TechTask.Data;
using TechTask.Interfaces;
using TechTask.Mapping;
using TechTask.Services;

namespace TechTask.Extensions;

public static class ApplicationServiceExtensions
{
	// Extention method for registering custom application services
	public static IServiceCollection AddApplicationServices(this IServiceCollection services,
			IConfiguration config)
	{
		services.AddSingleton<MongoDBContext>();

		services.AddAutoMapper(typeof(AutoMapperProfiles));
		services.AddScoped<ICustomersRepository, MongoCustomersRepository>();
		services.AddScoped<ICustomersService, CustomersService>();

		return services;
	}
}
