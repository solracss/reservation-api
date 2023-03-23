using Microsoft.EntityFrameworkCore;
using ReservationAPI.Data;

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
            var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
            builder.Services.AddDbContext<DataContext>(
                x => x.UseNpgsql(connectionString));
            builder.Services.AddScoped<DataSeeder>();

            var app = builder.Build();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
                seeder.SeedDataContext();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
