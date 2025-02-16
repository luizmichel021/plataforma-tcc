using plataformatcc.Interfaces;
using plataformatcc.Models;
using plataformatcc.Connection;
using MySql.Data.MySqlClient;


namespace Storages.StorageCustomer
{   

    public class StorageCustomer : ICustomerStorage
    {   
        private readonly Connection connection = new Connection();

        private readonly ILogger<StorageCustomer> _logger;

        public StorageCustomer(ILogger<StorageCustomer> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool create(Customer customer)
        {
            _logger.LogDebug("[Storage-Customer] - Starting create");
            using (var Connection = connection.GetConnection())
            {
                Connection.Open();
                _logger.LogInformation("[Storage-Customer] - Connection on database");
                var cmd = new MySqlCommand("INSERT INTO customers (name, surname, email, brithdate, created_at, update_at) VALUES (@name , @surname, @email, @brithdate, @created_at, @update_at)", Connection);
                cmd.Parameters.AddWithValue("@name", customer.Name);
                cmd.Parameters.AddWithValue("@surname", customer.Surname);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@brithdate", customer.Brithdate);
                cmd.Parameters.AddWithValue("@created_at", customer.Created_at);
                cmd.Parameters.AddWithValue("@update_at", customer.Update_at);

                var ret = cmd.ExecuteNonQuery();
                if(ret != 0)
                {   
                    _logger.LogInformation("[Storage-Customer] - User created in the database");
                    return true;
                    
                }
                else
                {
                    _logger.LogError("[Storage-Customer] - Error when finishing user creation");
                    return false;
                }
            }
        }

        public bool delete(int id)
        {   
            _logger.LogDebug("[Storage-Customer] - Starting delete");
            using(var Connection = connection.GetConnection())
            {
                Connection.Open();
                _logger.LogInformation("[Storage-Customer] - Connection on database");
                var cmd = new MySqlCommand("UPDATE customers SET active = false WHERE id = @id", Connection);
                cmd.Parameters.AddWithValue("@id", id);

                var ret = cmd.ExecuteNonQuery();

                if(ret != 0)
                {
                    _logger.LogInformation("[Storage-Customer] - User deleted/deactivated in the database");
                    return true;  
                }
                else
                {
                    _logger.LogError("[Storage-Customer] - Error when finishing user deleted/deactivated");
                    return false;
                }
            }
        }
        public Customer getCustomer(int id)
        {   
            _logger.LogDebug("[Storage-Customer] - Starting GetCustomer");
            using(var Connection = connection.GetConnection())
            {
                Connection.Open();
                _logger.LogInformation("[Storage-Customer] - Connection on database");

                var cmd = new MySqlCommand("SELECT name, surname, email, brithdate, created_at, update_at FROM customers ", Connection);
                cmd.Parameters.AddWithValue("@id", id);

                

                var ret = cmd.ExecuteReader();

                if(ret.Read())
                {
                    return new Customer
                    {
                        Id = ret.GetInt32("id"),
                        Name = ret.GetString("name"),
                        Surname = ret.GetString("surname"),
                        Email = ret.GetString("email"),
                        Brithdate = ret.GetDateTime("brithdate"),
                        Created_at = ret.GetDateTime("created_at"),
                        Update_at = ret.GetDateTime("update_at")
                    };
                }
                else
                {   
                    _logger.LogError("[Storage-Customer] - Error no customers found");
                    return new Customer {};
                }
            }
            
        }

        public List<Customer> getAllCustomers()
        {   
            using(var Connection = connection.GetConnection())
            {
                Connection.Open();
                _logger.LogInformation("[Storage-Customer] - Connection on Database ");
                
                var listCustomer = new List<Customer>();

                var cmd = new MySqlCommand("SELECT id, name, surname, email, brithdate, created_at, update_at FROM customers", Connection);

                var ret = cmd.ExecuteReader();
                while (ret.Read())
                {
                    listCustomer.Add(new Customer {
                        Id = ret.GetInt32("id"),
                        Name = ret.GetString("name"),
                        Surname = ret.GetString("surname"),
                        Email = ret.GetString("email"),
                        Brithdate = ret.GetDateTime("brithdate"),
                        Created_at = ret.GetDateTime("created_at"),
                        Update_at = ret.GetDateTime("update_at")
                    });
                }

                return listCustomer;

            }
        }

        public bool partialUpdate()
        {
            throw new NotImplementedException();
        }

        public bool update(Customer customer)
        {
            using(var Connection = connection.GetConnection())
            {
                Connection.Open();
                _logger.LogInformation("[Storage-Customer] - Connection on Database");
                var cmd = new MySqlCommand("UPDATE customers SET name = @name, surname = @surname, email = @email, brithdate = @brithdate WHERE id = @id", Connection);
                cmd.Parameters.AddWithValue("@id", customer.Id);
                cmd.Parameters.AddWithValue("@name", customer.Name);
                cmd.Parameters.AddWithValue("@surname", customer.Surname);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@brithdate", customer.Brithdate);

                var ret = cmd.ExecuteNonQuery();
                if (ret != 0)
                {
                    _logger.LogInformation("[Storage-Customer] - Customer update performed");
                    return true;
                }
                else
                {
                    _logger.LogError("[Storage-Customer] - Customer update not performed");
                    return false;
                }

            }
        }

   
    }
}