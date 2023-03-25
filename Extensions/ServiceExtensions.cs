using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReservationAPI.Data;
using ReservationAPI.Domain;
using ReservationAPI.Dtos;
using ReservationAPI.Middleware;
using ReservationAPI.Services;
using ReservationAPI.Validators;
using System.Reflection;

namespace ReservationAPI.Extensions;

public static class ServiceExtensions
{
    public static void RegisterDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DataContext>(
                x => x.UseNpgsql(connectionString));
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<DataSeeder>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAccountService, AccountService>();
    }

    public static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }

    public static void RegisterMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<ErrorHandlindMiddleware>();
    }

    public static void RegisterPasswordHasher(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
    }

    public static void RegisterValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
    }
}
