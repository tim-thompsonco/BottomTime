using BottomTimeAuth.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace BottomTimeAuth.Extensions {
	public static class ApplicationServiceExtensions {
		public static IServiceCollection AddApplicationServices(
			this IServiceCollection services, IConfiguration config) {
			services.AddDbContext<DataContext>(options => {
				// Check to see if env variable for DB is present, and if not, we are in local dev env using secret manager
				string connString = Environment.GetEnvironmentVariable("DATABASE_DOTNET_URL") ?? config["DATABASE_DOTNET_URL"];
				options.UseNpgsql(connString);
			});

			services.AddControllers()
				.AddNewtonsoftJson(options => {
					options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
					options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
				});

			services.AddAutoMapper(typeof(Startup));

			ConfigureSwaggerServices(services);

			return services;
		}

		private static IServiceCollection ConfigureSwaggerServices(IServiceCollection services) {
			services.AddSwaggerGen(c => {
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "BottomTimeAuth", Version = "v1" });
			});
			services.AddSwaggerGenNewtonsoftSupport();

			return services;
		}
	}
}
