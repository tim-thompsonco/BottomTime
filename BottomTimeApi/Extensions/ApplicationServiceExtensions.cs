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
				// Check to see if env variable for DB is present, and if not, we are in local dev env using secret manager
				string connString = Environment.GetEnvironmentVariable("DATABASE_DOTNET_URL") ?? config["DATABASE_DOTNET_URL"];
				options.UseNpgsql(connString);
				options.UseSnakeCaseNamingConvention();
			});

			return services;
		}
	}
}