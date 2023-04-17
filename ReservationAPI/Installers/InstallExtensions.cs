﻿namespace ReservationAPI.Installers
{
    public static class InstallExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(IInstaller).Assembly.GetTypes()
                .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>();

            foreach (var installer in installers)
            {
                installer.InstallServices(services, configuration);
            }
        }
    }
}
