using Microsoft.IdentityModel.Tokens;
using ReservationAPI.Authentication;
using System.Text;

namespace ReservationAPI.Extensions
{
    public static class AuthenticationConfig
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfigurationSection configurationSection)
        {
            var autenticationSettings = new AuthenticationSettings();

            configurationSection.Bind(autenticationSettings);

            services.AddSingleton(autenticationSettings);

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = autenticationSettings.JwtIssuer,
                    ValidAudience = autenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(autenticationSettings.JwtKey))
                };
            });
        }
    }
}
