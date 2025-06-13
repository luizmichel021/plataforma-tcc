using Interfaces;
using Models.Product;
using Connections;
using MySqlConnector;

namespace Repositories
{
    public class Repository : IRepository
    {
        private readonly Connection connection = new Connection();
        public bool Delete(Guid id)
        {
            using (var connection = this.connection.GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand("UPDATE products SET active = false WHERE id = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);
                var ret = cmd.ExecuteNonQuery();

                if (ret != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
        public List<Product> ListAllProducts()
        {
            using (var connection = this.connection.GetConnection())
            {
                connection.Open();
                var products = new List<Product>();
                var cmd = new MySqlCommand(
                    "SELECT id, name, description, price, quantity, created_at, update_at, active FROM products", connection
                );

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new Product()
                        {
                            Id = reader.GetGuid("id"),
                            Name = reader.GetString("name"),
                            Description = reader.GetString("description"),
                            Price = reader.GetFloat("price"),
                            Quantity = reader.GetInt32("quantity"),
                            Created_at = reader.GetDateTime("created_at"),
                            Update_at = reader.GetDateTime("update_at"),
                            Active = reader.GetBoolean("active")
                        };
                        products.Add(product);
                    }
                }

                return products;

            }


        }

        public List<Product> ListProductsFilter(string? name, string? description, float? pricemax, float? pricemin, int? quantity)
        {
            // SELECT * FROM products
            // WHERE 1=1
            // AND (@precoMin IS NULL OR price >= @precoMin)
            // AND (@precoMax IS NULL OR price <= @precoMax)
            // AND (@nome IS NULL OR name LIKE CONCAT('%', @nome, '%'))
            // AND (@descricao IS NULL OR description LIKE CONCAT('%', @descricao, '%'));
            // AND (@quantity IS NULL OR quantity = @quantity)

            using (var connection = this.connection.GetConnection())
            {
                connection.Open();
                var query = "SELECT * FROM products WHERE 1=1 ";
                var selection = new List<string>();
                var parameters = new List<MySqlParameter>();

                var ListProductsFilter = new List<Product>();

                if (!string.IsNullOrEmpty(name))
                {
                    selection.Add(" AND (@name IS NULL OR name LIKE CONCAT('%', @name, '%')) ");
                    parameters.Add(new MySqlParameter("@name", name));
                }
                if (!string.IsNullOrEmpty(description))
                {
                    selection.Add(" AND (@description IS NULL OR description LIKE CONCAT('%', description, '%')) ");
                    parameters.Add(new MySqlParameter("@description", description));
                }
                if (pricemax.HasValue)
                {
                    selection.Add(" AND (@priceMax IS NULL OR price <= @priceMax) ");
                    parameters.Add(new MySqlParameter("@priceMax", pricemax.Value));
                }
                if (pricemin.HasValue)
                {
                    selection.Add(" AND (@priceMin IS NULL OR price >= @priceMin) ");
                    parameters.Add(new MySqlParameter("@priceMin", pricemin.Value));
                }
                if (quantity.HasValue)
                {
                    selection.Add(" AND (@quantity IS NULL OR quantity = @quantity)");
                    parameters.Add(new MySqlParameter("@quantity", quantity.Value));
                }

                if (selection.Count == 0)
                {
                    return ListProductsFilter;
                }

                query += " " + string.Join(" ", selection);

                var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddRange(parameters.ToArray());


                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var product = new Product
                    {
                        Id = reader.GetGuid("id"),
                        Name = reader.GetString("name"),
                        Description = reader.GetString("description"),
                        Price = reader.GetFloat("price"),
                        Quantity = reader.GetInt32("quantity"),
                        Created_at = reader.GetDateTime("created_at"),
                        Update_at = reader.GetDateTime("update_at"),
                        Active = reader.GetBoolean("active")
                    };
                    ListProductsFilter.Add(product);
                }

                return ListProductsFilter;
            }
        }

        public Product Register(Product product)
        {
            using (var connection = this.connection.GetConnection())
            {
                connection.Open();
                var cmd = new MySqlCommand(
                "INSERT INTO products (id, name, description, price ,quantity) VALUES (@id, @name,  @description, @price, @quantity)", connection);
                cmd.Parameters.AddWithValue("@id", product.Id);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@description", product.Description);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@quantity", product.Quantity);
                var ret = cmd.ExecuteNonQuery();

                if (ret != 0)
                {
                    return product;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool Update(Guid id, string? name, string? description, float? price, int? quantity)
        {
            using (var connection = this.connection.GetConnection())
            {
                connection.Open();

                var query = "UPDATE products SET ";
                var updates = new List<string>();
                var parameters = new List<MySqlParameter>();

                if (!string.IsNullOrEmpty(name))
                {
                    updates.Add("name = @name");
                    parameters.Add(new MySqlParameter("@name", name));
                }

                if (!string.IsNullOrEmpty(description))
                {
                    updates.Add("description = @description");
                    parameters.Add(new MySqlParameter("@description", description));
                }

                if (price.HasValue)
                {
                    updates.Add("price = @price");
                    parameters.Add(new MySqlParameter("@price", price.Value));
                }

                if (quantity.HasValue)
                {
                    updates.Add("quantity = @quantity");
                    parameters.Add(new MySqlParameter("@quantity", quantity.Value));
                }

                // Se não tem nada pra atualizar, não faz nada
                if (updates.Count == 0)
                {
                    return false;
                }

                query += string.Join(", ", updates) + " WHERE id = @id";
                parameters.Add(new MySqlParameter("@id", id));

                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddRange(parameters.ToArray());

                var ret = cmd.ExecuteNonQuery();
                return ret > 0;
            }       
        }
    }
}
