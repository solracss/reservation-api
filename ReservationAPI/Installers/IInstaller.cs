namespace ReservationAPI.Installers
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection Services, IConfiguration Configuration);
    }
}
