using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Utils.DBContext
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("mysql")!;
        }

        public IDbConnection CreateConnection() => new MySqlConnection(connectionString);
    }
}
