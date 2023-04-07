using FluentValidation.AspNetCore;
using ReservationAPI.Extensions;
using ReservationAPI.Middleware;

namespace ReservationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureAuthentication(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.RegisterDatabase(builder
                .Configuration);
            builder.Services.RegisterServices();
            builder.Services.AddInfrastructure();

            builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            builder.Services.ApiVersionConfiguration();

            var app = builder.Build();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
                seeder.SeedDataContext();
            }

            app.ConfigureSwagger();
            app.UseErrorHandlindMiddleware();
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
