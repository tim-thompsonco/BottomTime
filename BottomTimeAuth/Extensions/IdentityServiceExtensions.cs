using BottomTimeAuth.Data;
using BottomTimeAuth.Enums;
using BottomTimeAuth.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BottomTimeAuth.Extensions {
	public static class IdentityServiceExtensions {
		public static IServiceCollection AddIdentityServices(
			this IServiceCollection services, IConfiguration config) {
			services.AddIdentityCore<AppUser>(options => {
				options.Password.RequireNonAlphanumeric = false;
			})
			  .AddRoles<AppRole>()
			  .AddRoleManager<RoleManager<AppRole>>()
			  .AddSignInManager<SignInManager<AppUser>>()
			  .AddRoleValidator<RoleValidator<AppRole>>()
			  .AddEntityFrameworkStores<DataContext>();

			services.AddAuthentication(options => {
				options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			});

			services.AddAuthorization(options => {
				options.AddPolicy("RequireAdminRole", policy => policy.RequireRole(AppRoleCategory.Admin.ToString()));
			});

			return services;
		}
	}
}
