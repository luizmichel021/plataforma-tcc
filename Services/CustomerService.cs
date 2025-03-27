using plataformatcc.Interfaces;
using plataformatcc.Models;
using Repositories.CustomerRepository;


namespace plataformatcc.Service
{
     
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger ;
        private readonly CustomerRepository _repository ;
        
        public CustomerService(ILogger<CustomerService> logger, CustomerRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }  
        
        
        public Customer create(Customer customer)
        {   
            _logger.LogDebug("[Service-Customer] - Trying to create product");
            try
            {   
                var ret = _repository.create(customer);
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
                if(_repository.delete(id))
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
                var ret = _repository.getAllCustomers();
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
                var ret = _repository.getCustomer(id);
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

        public bool partialUpdate(Guid id, string? name, string? surname, string? email, DateTime? birthdate)
        {
            _logger.LogInformation("[Service-Customer] -  trying to UpdatePartial to Customer");
            try
            {
                _logger.LogInformation("Successfully initiated retrieval of customer update partial.");
                _logger.LogInformation($"[Service-Customer] - Received data for update: id={id}, name={name}, surname={surname}, email={email}, birthdate={birthdate}");
                var ret =  _repository.partialUpdate(id, name, surname, email, birthdate); 
                if(ret == false)
                {
                   _logger.LogWarning("[Service-Customer] - The storage returned false, check the logic or the provided data.");
                   return false;
                }
                
                return true;
    
            }
            catch(Exception e)
            {
                _logger.LogError(e, "[Service-Customer] - Exception during to Update to Customer. Error : {Exception}", e.Message);
                throw;
            }            
        }

        public bool update(Guid id, Customer customer)
        {
            _logger.LogDebug("[Service-Customer] - Trying to update Customer");
            try
            {
                _logger.LogInformation("Successfully initiated retrieval of customer update.");
                
                var ret = _repository.update(id,customer);
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