using BottomTimeApi.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BottomTimeApi.Extensions {
	public static class ApplicationServiceExtensions {
		public static IServiceCollection AddApplicationServices(
			this IServiceCollection services, IConfiguration config) {
			services.AddScoped<IDiveRepository, DiveRepository>();
			services.AddDbContext<DataContext>(options => {
				string sqlConnectionString = config.GetConnectionString("DefaultConnection");
				options.UseNpgsql(sqlConnectionString);
				options.UseLowerCaseNamingConvention();
			});

			return services;
		}
	}
}