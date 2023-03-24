using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReservationAPI.Data;
using ReservationAPI.Domain;
using ReservationAPI.Middleware;
using ReservationAPI.Services;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using ReservationAPI.Dtos;
using ReservationAPI.Validators;

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
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddScoped<ErrorHandlindMiddleware>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

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

            app.UseErrorHandlindMiddleware();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
