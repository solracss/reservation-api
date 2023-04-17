namespace Infrastructure.Data
{
    public interface IDbConnectionStringProvider
    {
        string GetConnectionString();
    }
}
