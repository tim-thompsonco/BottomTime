using BottomTimeApi.Extensions;
using BottomTimeApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BottomTimeApi {
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
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BottomTimeApi v1"));

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}
