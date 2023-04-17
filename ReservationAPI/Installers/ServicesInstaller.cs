using Infrastructure;
using Application;
using Application.Authorization;

namespace ReservationAPI.Installers
{
    public class ServicesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication(configuration);
            services.RegisterDatabase(configuration);
            services.AddControllers();
            services.ApiVersionConfiguration();
            services.AddApiDocumentation();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Over 18", policy =>
                {
                    policy.Requirements.Add(new AgeRequirement(18));
                });
            });
        }
    }
}
