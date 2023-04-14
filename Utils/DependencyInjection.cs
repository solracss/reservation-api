﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReservationAPI.Authorization;
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
    public static IServiceCollection RegisterDatabase(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<DataContext>(
                x => x.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]));

        return services;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<DataSeeder>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IHttpContextService, HttpContextService>();
        services.AddScoped<IAuthorizationHandler, AgeRequirementHandler>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<ErrorHandlindMiddleware>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
        services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
        services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        return services;
    }

    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("Over 18", policy =>
            {
                policy.Requirements.Add(new AgeRequirement(18));
            });
        });
        return services;
    }
}