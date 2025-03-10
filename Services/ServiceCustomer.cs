using plataformatcc.Interfaces;
using plataformatcc.Models;
using Storages.StorageCustomer;

namespace plataformatcc.Service
{
     
    public class ServiceCustomer : ICustomerService
    {
        private readonly ILogger<ServiceCustomer> _logger ;
        private readonly StorageCustomer _storage ;
        
        public ServiceCustomer(ILogger<ServiceCustomer> logger, StorageCustomer storage)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }  
        
        
        public Customer create(Customer customer)
        {   
            _logger.LogDebug("[Service-Customer] - Trying to create product");
            try
            {   
                var ret = _storage.create(customer);
                if(ret != null)
                {
                    _logger.LogInformation("[Service-Customer] - Customer created a success");
                    return ret;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "[Service-Customer] - Exception during Customer creation. Error: {Exception}", e.Message);
                return null;
            }
            return null;
        }

        public bool delete(Guid id)
        {
            _logger.LogDebug("[Service-Customer] - trying to delete to Customer.");
            try
            {
                if(_storage.delete(id))
                {
                    _logger.LogInformation("[Service-Customer]- Customer deleted/deactivated a Success");
                    return true;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "[Service-Customer] - Exception during the deleted/deactivated. Error : {Exception}", e.Message);
            }
            return false;
        }


    
        public List<Customer> getAllCustomers()
        {
            _logger.LogDebug("[Service-Customer] - Trying to Get All Customers");

            try
            {
                var ret = _storage.getAllCustomers();
                if(ret != null)
                _logger.LogInformation("[Service-Customer] - get all customer as a success.");
                return ret;
            }
            catch (Exception e)
            {                
               _logger.LogError(e,"[Service-Customer] - Exception during to get all Customers. Error : {Exception}", e.Message);
            }
            return null;
        }

        public Customer getCustomer(Guid id)
        {
            _logger.LogDebug("[Service-Customer] - Attempting to retrieve customer with ID: {CustomerId}", id);
            try
            {
                _logger.LogInformation("[Service-Customer] - Successfully initiated retrieval of customer with ID: {CustomerId}", id);
                var ret = _storage.getCustomer(id);
                 if (ret== null)
                {
                _logger.LogWarning("[Service-Customer] - No");               
                }
                else
                {
                    return ret;
                }
            }
            catch (Exception e)
            {                
               _logger.LogError(e,"[Service-Customer] - Exception during to get to Customer. Error : {Exception}", e.Message);
            }
            return null;
        }

        public bool update(Guid id, Customer customer)
        {
            _logger.LogDebug("[Service-Customer] - Trying to update Customer");
            try
            {
                _logger.LogInformation("Successfully initiated retrieval of customer update.");
                
                var ret = _storage.update(id,customer);
                if(ret == true)
                {
                    _logger.LogInformation("[Service-Customer] - Customer update success.");
                    return true;
                    
                }
                else
                {
                    _logger.LogWarning("[Service-Customer] - Customer update not carried out review the data ");
                    return false;
                }

            }
            catch(Exception e)
            {
                _logger.LogError(e, "[Service-Customer] - Exception during to Update to Customer. Error : {Exception}", e.Message);
            }
            return false;           
        }
    }
}