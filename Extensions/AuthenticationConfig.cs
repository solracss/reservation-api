using Microsoft.IdentityModel.Tokens;
using ReservationAPI.Authentication;
using System.Text;

namespace ReservationAPI.Extensions
{
    public static class AuthenticationConfig
    {
        public static void ConfigureAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            var autenticationSettings = new AuthenticationSettings();

            configuration.GetSection(AuthenticationSettings.SectionName).Bind(autenticationSettings);

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
