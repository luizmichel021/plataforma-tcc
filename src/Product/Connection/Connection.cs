using MySqlConnector;

namespace Connections
{
    public class Connection
    {
        private string _connectionString = "Server=localhost;Database=productdb;Uid=root;Pwd=123456;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}