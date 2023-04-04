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

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.RegisterDatabase(builder
                .Configuration["ConnectionStrings:DefaultConnection"]);
            builder.Services.RegisterServices();
            builder.Services.RegisterAutoMapper();
            builder.Services.RegisterMiddlewares();
            builder.Services.RegisterPasswordHasher();
            builder.Services.RegisterValidators();
            builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

            var app = builder.Build();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
                seeder.SeedDataContext();
            }

            app.ConfigureSwagger();
            app.UseErrorHandlindMiddleware();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
