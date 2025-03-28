using Microsoft.AspNetCore.Mvc;
using plataformatcc.Models;
using plataformatcc.Service;
using Repositories.CustomerRepository;

namespace plataformatcc.ControllerCustomer
{
    [ApiController]
    [Route("plataformatcc/customer")]
    public class ControllerCustomer : ControllerBase
    {
        private readonly CustomerRepository _CustomerRepository;
        
        public ControllerCustomer(CustomerRepository serivceCustomer)
        {
            _CustomerRepository = serivceCustomer ?? throw new ArgumentNullException(nameof(serivceCustomer));
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            var ret = _CustomerRepository.create(customer);
            if (ret != null)
            {
                return CreatedAtAction(nameof(Get), new { id = ret.Id }, ret);
            }
            else
            {
                return BadRequest(new {message = "[Controller-Customer] - Fail to Create Customer."});
            }
        }

        [HttpDelete("{id}")]        
        public ActionResult Delete(Guid id)
        {
            var ret = _CustomerRepository.delete(id);
            if(ret == true)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(new {message = "[Controller-Customer] - fail to triyng to delet the customer."});
            }
        }

        
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var ret = _CustomerRepository.getCustomer(id);
            if(ret != null)
            {
                return Ok(ret);
            }
            else
            {
                return BadRequest(new {message = "[Controller-Customer] - Fail to trying to get Custumer."});
            }
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var customers = _CustomerRepository.getAllCustomers();
            return Ok(customers);
        }

        [HttpPut("{id}")]
        public ActionResult Update(Guid id, Customer customer)
        {
            var ret = _CustomerRepository.update(id, customer);
            if(ret == true)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(new {message = "[Controller-Customer] - Fail to trying to Update Custumer."});
            }
            
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdate(Guid id, Customer customer)
        {   
            
            var ret = _CustomerRepository.partialUpdate(id, customer.Name, customer.Surname, customer.Email, customer.Birthdate);
            if (ret)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(new {message = "[Controller-Customer] - Fail to trying to Update Customer."});
            }

        }

    }
}