using Interfaces;
using Models.Product;
using Connections;
using MySqlConnector;

// LOG REPOSITORY FEITO.

namespace Repositories
{
    public class Repository : IRepository
    {
        private readonly Connection connection = new Connection();
        private readonly ILogger<Repository> _logger;
        public Repository(ILogger<Repository>? logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public bool Delete(Guid id)
        {
            _logger.LogDebug("[Repository-Product] - Starting a connection on database.");
            using (var connection = this.connection.GetConnection())
            {
                _logger.LogInformation("[Repository-Product] - Successfully connected to the database.");
                connection.Open();
                var cmd = new MySqlCommand("UPDATE products SET active = false WHERE id = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);
                _logger.LogDebug("[Repository-Product] - Executing logical deletion of the product.");
                var ret = cmd.ExecuteNonQuery();
                if (ret != 0)
                {
                    _logger.LogInformation("[Repository-Product] - Product deactivated successfully.");
                    return true;
                }
                else
                {
                    _logger.LogError("[Repository-Product] - Failed to deactivate the product.");
                    return false;
                }

            }
        }
        public List<Product> ListAllProducts()
        {
            _logger.LogDebug("[Repository-Product] - Starting a connection on database.");
            using (var connection = this.connection.GetConnection())
            {
                connection.Open();
                _logger.LogInformation("[Repository-Product] - Successfully connected to the database.");
                var products = new List<Product>();
                var cmd = new MySqlCommand(
                    "SELECT id, name, description, price, quantity, created_at, update_at, active FROM products", connection
                );
                _logger.LogInformation("[Repository-Product] - incializing construct the list of products");
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
            _logger.LogDebug("[Repository-Product] - Starting a connection on database.");
            using (var connection = this.connection.GetConnection())
            {
                connection.Open();
                _logger.LogInformation("[Repository-Product] - Successfully connected to the database.");

                var query = "SELECT * FROM products WHERE 1=1 ";
                var selection = new List<string>();
                var parameters = new List<MySqlParameter>();

                var ListProductsFilter = new List<Product>();

                if (!string.IsNullOrEmpty(name))
                {
                    selection.Add(" AND (@name IS NULL OR name LIKE CONCAT('%', @name, '%')) ");
                    parameters.Add(new MySqlParameter("@name", name));
                    _logger.LogDebug($"[Repository-Product] - Filter applied: name LIKE '%{name}'");
                }
                if (!string.IsNullOrEmpty(description))
                {
                    selection.Add(" AND (@description IS NULL OR description LIKE CONCAT('%', description, '%')) ");
                    parameters.Add(new MySqlParameter("@description", description));
                    _logger.LogDebug($"[Repository-Product] - Filter applied: description LIKE '%{description}%'");
                }
                if (pricemax.HasValue)
                {
                    selection.Add(" AND (@priceMax IS NULL OR price <= @priceMax) ");
                    parameters.Add(new MySqlParameter("@priceMax", pricemax.Value));
                    _logger.LogDebug($"[Repository-Product] - Filter applied: price <= {pricemax.Value}");
                }
                if (pricemin.HasValue)
                {
                    selection.Add(" AND (@priceMin IS NULL OR price >= @priceMin) ");
                    parameters.Add(new MySqlParameter("@priceMin", pricemin.Value));
                    _logger.LogDebug($"[Repository-Product] - Filter applied: price >= {pricemin.Value}");
                }
                if (quantity.HasValue)
                {
                    selection.Add(" AND (@quantity IS NULL OR quantity = @quantity)");
                    parameters.Add(new MySqlParameter("@quantity", quantity.Value));
                    _logger.LogDebug($"[Repository-Product] - Filter applied: quantity = {quantity.Value}");
                }

                if (selection.Count == 0)
                {
                    _logger.LogWarning("[Repository-Product] - no filter provided, empty list will be returned.");
                    return ListProductsFilter;
                }

                query += " " + string.Join(" ", selection);

                _logger.LogInformation("[Repository-Product] - Executing query to fetch products with filters.");
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
            try
            {
                _logger.LogDebug("[Repository-Product] - Starting a connection on database.");

                using (var connection = this.connection.GetConnection())
                {
                    connection.Open();
                    _logger.LogInformation("[Repository-Product] - Successfully connected to the database.");

                    _logger.LogDebug($"[Repository-Product] - starting product registration: Name={product.Name}, Price={product.Price}, Quantity={product.Quantity}");
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
                        _logger.LogInformation("[Repository-Product] - Product successfully registered in the database.");
                        return product;
                    }
                    else
                    {
                        _logger.LogWarning("[Repository-Product] - Product registration failed, no rows affected.");
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "[Repository-Product] - Exception occurred while registering the product.");
                throw;
            }

        }

        public bool Update(Guid id, string? name, string? description, float? price, int? quantity)
        {
            _logger.LogDebug("[Repository-Product] - Starting a connection on the database.");
            using (var connection = this.connection.GetConnection())
            {
                connection.Open();
                _logger.LogInformation("[Repository-Product] - Successfully connected to the database.");

                var query = "UPDATE products SET ";
                var updates = new List<string>();
                var parameters = new List<MySqlParameter>();

                if (!string.IsNullOrEmpty(name))
                {
                    updates.Add("name = @name");
                    parameters.Add(new MySqlParameter("@name", name));
                    _logger.LogDebug($"[Repository-Product] - change applied: name LIKE '%{name}'");
                }

                if (!string.IsNullOrEmpty(description))
                {
                    updates.Add("description = @description");
                    parameters.Add(new MySqlParameter("@description", description));
                    _logger.LogDebug($"[Repository-Product] - change applied: description LIKE '%{description}'");
                }

                if (price.HasValue)
                {
                    updates.Add("price = @price");
                    parameters.Add(new MySqlParameter("@price", price.Value));
                    _logger.LogDebug($"[Repository-Product] - change applied: price LIKE '%{price}'");

                }

                if (quantity.HasValue)
                {
                    updates.Add("quantity = @quantity");
                    parameters.Add(new MySqlParameter("@quantity", quantity.Value));
                    _logger.LogDebug($"[Repository-Product] - change applied: quantity LIKE '%{quantity}'");
                }


                if (updates.Count == 0)
                {
                    _logger.LogError("[Repository-Product] - no changes detected.");
                    return false;
                }

                query += string.Join(", ", updates) + " WHERE id = @id";
                parameters.Add(new MySqlParameter("@id", id));

                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddRange(parameters.ToArray());

                var ret = cmd.ExecuteNonQuery();
                _logger.LogInformation("[Repository-Product] - update carried out successfully.")

                return ret > 0;
            }
        }
    }
}
