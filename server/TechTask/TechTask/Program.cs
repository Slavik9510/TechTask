using TechTask.Exceptions.Handler;
using TechTask.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Adding application-specific services
builder.Services.AddApplicationServices(builder.Configuration);

// Configuring a custom exception handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyMethod()
			  .AllowAnyHeader();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.UseExceptionHandler(options => { });

app.Run();