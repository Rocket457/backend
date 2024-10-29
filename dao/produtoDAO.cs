using MySql.Data.MySqlClient;
using crud.produto.Models;
using crud.produto.connector;

namespace crud.produto.DAO
{
    public class ItemDAO
    {
        private readonly Connector _connector = new();
        private readonly ILogger<ItemDAO> _logger;

        public ItemDAO(ILogger<ItemDAO> logger)
        {
            _logger = logger;
        }

        public List<Item> GetItens()
        {
            var itens = new List<Item>();
            using (var connection = _connector.GetConnection())
            {
                var command = new MySqlCommand("SELECT Id, Nome, Preco FROM produtos", connection);
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var item = new Item
                    {
                        Id = reader.GetInt32("Id"),
                        Nome = reader.GetString("Nome"),
                        Preco = reader.GetDecimal("Preco")
                    };
                    itens.Add(item);
                }
            }
            return itens;
        }

        public int CreateItem(Item item)
        {
            using var connection = _connector.GetConnection();
            var command = new MySqlCommand("INSERT INTO produtos (Nome, Preco) VALUES (@Nome, @Preco); SELECT LAST_INSERT_ID();", connection);
            command.Parameters.AddWithValue("@Nome", item.Nome);
            command.Parameters.AddWithValue("@Preco", item.Preco);
            var createdId = Convert.ToInt32(command.ExecuteScalar()); // Captura o ID criado
            return createdId;
        }

        public Item? GetItemById(int id)
        {
            Item? item = null;
            using var connection = _connector.GetConnection();
            var command = new MySqlCommand("SELECT Id, Nome, Preco FROM produtos WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                item = new Item
                {
                    Id = reader.GetInt32("Id"),
                    Nome = reader.GetString("Nome"),
                    Preco = reader.GetDecimal("Preco")
                };
            }
            return item;
        }

        public void UpdateItem(int id, Item item)
        {
            using var connection = _connector.GetConnection();
            var command = new MySqlCommand("UPDATE produtos SET Nome = @Nome, Preco = @Preco WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Nome", item.Nome);
            command.Parameters.AddWithValue("@Preco", item.Preco);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }

        public void DeleteItem(int id)
        {
            using var connection = _connector.GetConnection();
            var command = new MySqlCommand("DELETE FROM produtos WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }
    }
}
