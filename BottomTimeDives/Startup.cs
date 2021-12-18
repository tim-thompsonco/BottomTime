using BottomTimeDives.Extensions;
using BottomTimeDives.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BottomTimeDives {
	public class Startup {
		private readonly IConfiguration _config;

		public Startup(IConfiguration config) {
			_config = config;
		}

		public void ConfigureServices(IServiceCollection services) {
			services.AddApplicationServices(_config);
		}

		public void Configure(IApplicationBuilder app) {
			app.UseMiddleware<ExceptionMiddleware>();

			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BottomTimeDives v1"));

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}
