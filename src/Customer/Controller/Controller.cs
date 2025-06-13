using Microsoft.AspNetCore.Mvc;
using Model;
using Services;

namespace Controller
{
    [ApiController]
    [Route("customer")]
    public class Controller : ControllerBase
    {
        private readonly Service _service;

        public Controller(Service service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            var ret = _service.create(customer);
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
            var ret = _service.delete(id);
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
            var ret = _service.getCustomer(id);
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
            var customers = _service.getAllCustomers();
            return Ok(customers);
        }

        [HttpPut("{id}")]
        public ActionResult Update(Guid id, Customer customer)
        {
            var ret = _service.update(id, customer);
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
            
            var ret = _service.partialUpdate(id, customer.Name, customer.Surname, customer.Email, customer.Birthdate);
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