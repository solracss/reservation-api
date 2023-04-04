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

public static class DependencyInjection
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

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<ErrorHandlindMiddleware>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
        return services;
    }
}
