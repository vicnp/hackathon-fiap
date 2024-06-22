using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace LHS_IOT.DBContext
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration; 
        private readonly string connectionString;
        public DapperContext(IConfiguration configuration)
        {

            _configuration = configuration;
            connectionString = configuration.GetConnectionString("mysql");
        }

        public IDbConnection CreateConnection() =>new MySqlConnection(connectionString);
    }
}
