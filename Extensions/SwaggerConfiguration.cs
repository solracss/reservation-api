using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace ReservationAPI.Extensions
{
    public static class SwaggerConfiguration
    {
        public static WebApplication ConfigureSwagger(this WebApplication app)
        {
            var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });
            }

            return app;
        }

        public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.ConfigureOptions<ConfigureSwaggerOptions>();

            return services;
        }
    }
}
