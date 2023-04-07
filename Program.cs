using ReservationAPI.Extensions;
using ReservationAPI.Middleware;

namespace ReservationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services
                .ConfigureAuthentication(builder.Configuration)
                .RegisterDatabase(builder.Configuration)
                .RegisterServices()
                .AddInfrastructure()
                .ApiVersionConfiguration()
                .AddApiDocumentation();

            var app = builder.Build();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
                seeder.SeedDataContext();
            }

            app
                .ConfigureSwagger()
                .UseErrorHandlindMiddleware()
                .UseAuthentication()
                .UseHttpsRedirection()
                .UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
