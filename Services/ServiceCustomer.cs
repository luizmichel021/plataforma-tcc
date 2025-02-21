using plataformatcc.Interfaces;
using plataformatcc.Models;
using Storages.StorageCustomer;

namespace plataformatcc.Service
{
     
    public class ServiceCustomer : ICustomerService
    {
        private readonly ILogger<ServiceCustomer> _logger;
        private readonly StorageCustomer storage = new StorageCustomer();
        
        public ServiceCustomer(ILogger<ServiceCustomer> logger, StorageCustomer storage)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
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
            _logger.LogDebug("[Service-Customer] - trying to delete to Customer.");
            try
            {
                var ret = storage.delete(id);
                if(ret != false)
                {
                    _logger.LogInformation("[Service-Customer]- Customer deleted/deactivated a Success");
                    return id;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "[Service-Customer] - Exception during the deleted/deactivated. Error : {Exception}", e.Message);
            }
            return 0;
        }

        public List<Customer> getAllCustomers()
        {
            throw new NotImplementedException();
        }

        public Customer getCustomer(int id)
        {
            _logger.LogDebug("[Service-Customer] - trying to Get to Customer.");
            try
            {
                _logger.LogInformation("[Service-Customer] - get to Customer.");
                return storage.getCustomer(id);
                
            }
            catch (Exception e)
            {                
               _logger.LogError(e,"[Service-Customer] - Exception during to get to Customer. Error : {Exception}", e.Message);
            }
            return null;

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