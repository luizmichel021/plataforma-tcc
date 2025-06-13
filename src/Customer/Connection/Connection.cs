using MySql.Data.MySqlClient;

namespace Connections
{
    public class Connection
    {
        private string _connectionString = "Server=localhost;Database=plataformatcc;Uid=root;Pwd=123456;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}