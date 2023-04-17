using Application.Middleware;
using ReservationAPI.Installers;

namespace ReservationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;
            services.InstallServicesInAssembly(builder.Configuration);

            var app = builder.Build();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            app.ConfigureSwagger()
                .UseMiddleware<ErrorHandlingMiddleware>()
                .UseAuthentication()
                .UseHttpsRedirection()
                .UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
