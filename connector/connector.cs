using MySql.Data.MySqlClient;

namespace crud.produto.connector
{
    public class Connector
    {
        private readonly string _connectionString;

        public Connector()
        {
            // Definindo a string de conexão
            _connectionString = "Server=junction.proxy.rlwy.net;Port=30342;Database=railway;Uid=root;Pwd=ZYifapejMYlaLWzcGyFRCgsbNcIpiMNs;";
        }

        public MySqlConnection GetConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
