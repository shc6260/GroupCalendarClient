using Microsoft.Data.SqlClient;

namespace GroupCalendar.Core.Helpers
{
    // DapperContext를 DbConnectionFactory로 변경
    public class DbConnectionFactory
    {
        private const string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=GroupCalendar;Integrated Security=true;";

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }

}
