
using plataformatcc.Interfaces;
using plataformatcc.Models;
using Storages.StorageCustomer;

namespace plataformatcc.Service.Ser
{
     
    public class ServiceCustomer : ICustomerService
    {
        private readonly ILogger<ServiceCustomer> _logger;
        private readonly StorageCustomer storage = new StorageCustomer();
        
        public ServiceCustomer(ILogger<ServiceCustomer> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }   
        
        
        public Customer? create(Customer customer)
        {   
            _logger.LogDebug("[Service-Customer] - Trying to create product");
            try
            {   
                var ret = storage.create(customer);
                if(ret != null)
                {
                    _logger.LogInformation("[Service-Customer] - Customer created a success");
                }
                return ret;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "[Service-Customer] - Exception during Customer creation. Error: {Exception}", e.Message);
            }
            return null;
        }

        public int delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Customer> getAllCustomers()
        {
            throw new NotImplementedException();
        }

        public Customer getCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public bool partialUpdate()
        {
            throw new NotImplementedException();
        }

        public Customer update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}