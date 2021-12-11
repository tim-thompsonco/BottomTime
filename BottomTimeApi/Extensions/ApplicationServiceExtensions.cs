using BottomTimeApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BottomTimeApi.Extensions {
	public static class ApplicationServiceExtensions {
		public static IServiceCollection AddApplicationServices(
			this IServiceCollection services, IConfiguration config) {
			services.AddScoped<IDiveRepository, DiveRepository>();
			services.AddDbContext<DataContext>(options => {
				string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
				string connString = env == "Development" ? config["DATABASE_DOTNET_URL"]
					: Environment.GetEnvironmentVariable("DATABASE_DOTNET_URL");
				options.UseNpgsql(connString);
				options.UseSnakeCaseNamingConvention();
			});

			return services;
		}
	}
}