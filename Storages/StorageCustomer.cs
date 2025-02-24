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

       

        public StorageCustomer(ILogger<StorageCustomer>? logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Customer? create(Customer customer)
        {
            _logger.LogDebug("[Storage-Customer] - Starting create");
            using (var Connection = connection.GetConnection())
            {
                Connection.Open();
                _logger.LogInformation("[Storage-Customer] - Connection on database");
                var cmd = new MySqlCommand(
                "INSERT INTO customers (name, surname, email, birthdate) VALUES (@name , @surname, @email, @birthdate);"
                + 
                "SELECT id, name, surname, email, birthdate, created_at, update_at, active FROM customers WHERE id = LAST_INSERT_ID();", 
                Connection
                );
                cmd.Parameters.AddWithValue("@name", customer.Name);
                cmd.Parameters.AddWithValue("@surname", customer.Surname);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@birthdate", customer.Birthdate);

                var ret = cmd.ExecuteReader();

                if (ret.Read())
                {   
                    _logger.LogInformation("[Storage-Customer] - Customer creation was a success.");
                    return new Customer 
                    {
                        Id = ret.GetInt32("id"),
                        Name = ret.GetString("name"),
                        Surname = ret.GetString("surname"),
                        Email = ret.GetString("email"),
                        Birthdate = ret.GetDateTime("birthdate"),
                        Created_at = ret.GetDateTime("created_at"),
                        Update_at = ret.GetDateTime("update_at"),
                        Active = ret.GetBoolean("active")
                    };                    
                }
                else
                {
                    _logger.LogWarning("[Storage-Customer] - Customer creation failed, returning null.");
                    return null;
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

        public Customer? getCustomer(int id)
        {   
            _logger.LogDebug("[Storage-Customer] - Starting GetCustomer");
            using(var Connection = connection.GetConnection())
            {
                Connection.Open();
                _logger.LogInformation("[Storage-Customer] - Connection on database");

                var cmd = new MySqlCommand("SELECT id, name, surname, email, birthdate, created_at, update_at, active FROM customers WHERE id = @id ", Connection);
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
                        Birthdate = ret.GetDateTime("birthdate"),
                        Created_at = ret.GetDateTime("created_at"),
                        Update_at = ret.GetDateTime("update_at"),
                        Active = ret.GetBoolean("active")
                    };
                }
                else
                {   
                    _logger.LogError("[Storage-Customer] - Error no customers found");
                    return null;
                }
            }
            
        }

        public List<Customer>? getAllCustomers()
        {   
            using(var Connection = connection.GetConnection())
            {
                Connection.Open();
                _logger.LogInformation("[Storage-Customer] - Connection on Database ");
                
                var listCustomer = new List<Customer>();

                var cmd = new MySqlCommand("SELECT id, name, surname, email, birthdate, created_at, update_at, active FROM customers", Connection);

                var ret = cmd.ExecuteReader();
                while (ret.Read())
                {
                    listCustomer.Add(new Customer {
                        Id = ret.GetInt32("id"),
                        Name = ret.GetString("name"),
                        Surname = ret.GetString("surname"),
                        Email = ret.GetString("email"),
                        Birthdate = ret.GetDateTime("birthdate"),
                        Created_at = ret.GetDateTime("created_at"),
                        Update_at = ret.GetDateTime("update_at"),
                        Active = ret.GetBoolean("active")
                    });
                }

                return listCustomer;

            }
        }

        public bool partialUpdate()
        {
            throw new NotImplementedException();
        }

        public bool update(int id,Customer customer)
        {
            using(var Connection = connection.GetConnection())
            {
                Connection.Open();
                _logger.LogInformation("[Storage-Customer] - Connection on Database");
                var cmd = new MySqlCommand("UPDATE customers SET name = @name, surname = @surname, email = @email, birthdate = @birthdate ,update_at = NOW() WHERE id = @id ", Connection);
                cmd.Parameters.AddWithValue("@name", customer.Name);
                cmd.Parameters.AddWithValue("@surname", customer.Surname);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@birthdate", customer.Birthdate);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@update_at", DateTime.Now);

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