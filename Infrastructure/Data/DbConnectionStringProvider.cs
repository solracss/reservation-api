namespace Infrastructure.Data
{
    public class DbConnectionStringProvider : IDbConnectionStringProvider
    {
        private readonly string _connectionString;

        public DbConnectionStringProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
