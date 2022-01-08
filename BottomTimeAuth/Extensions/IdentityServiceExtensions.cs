using BottomTimeAuth.Data;
using BottomTimeAuth.Enums;
using BottomTimeAuth.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BottomTimeAuth.Extensions {
	public static class IdentityServiceExtensions {
		public static IServiceCollection AddIdentityServices(
			this IServiceCollection services, IConfiguration config) {
			/*services.AddIdentityCore<AppUser>(options => {
				options.Password.RequireNonAlphanumeric = false;
			})
			  .AddRoles<AppRole>()
			  .AddRoleManager<RoleManager<AppRole>>()
			  .AddSignInManager<SignInManager<AppUser>>()
			  .AddRoleValidator<RoleValidator<AppRole>>()
			  .AddEntityFrameworkStores<DataContext>();*/

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options => {
					options.TokenValidationParameters = new TokenValidationParameters {
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(
							Encoding.UTF8.GetBytes(config["TokenKey"])),
						ValidateIssuer = false,
						ValidateAudience = false
					};
				});

			services.AddAuthorization(options => {
				options.AddPolicy("RequireAdminRole", policy => policy.RequireRole(AppRoleCategory.Admin.ToString()));
			});

			return services;
		}
	}
}
