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
                var cmd = new MySqlCommand("INSERT INTO customer (name, surname, email, brithdate, created_at, update_at) VALUES (@name , @surname, @email, @brithdate, @created_at, @update_at)", Connection);
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
            throw new NotImplementedException();
        }

        public List<Customer> listCustomers()
        {
            throw new NotImplementedException();
        }

        public bool partialUpdate()
        {
            throw new NotImplementedException();
        }

        public bool update()
        {
            throw new NotImplementedException();
        }
    }
}