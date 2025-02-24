using Microsoft.AspNetCore.Mvc;
using plataformatcc.Models;
using plataformatcc.Service;

namespace plataformatcc.ControllerCustomer
{
    [ApiController]
    [Route("plataformatcc/customer")]
    public class ControllerCustomer : ControllerBase
    {
        private readonly ServiceCustomer _serviceCustomer;
        
        public ControllerCustomer(ServiceCustomer serivceCustomer)
        {
            _serviceCustomer = serivceCustomer ?? throw new ArgumentNullException(nameof(serivceCustomer));
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            var ret = _serviceCustomer.create(customer);
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
        public ActionResult Delete(int id)
        {
            var ret = _serviceCustomer.delete(id);
            if(ret == id)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(new {message = "[Controller-Customer] - fail to triyng to delet the customer."});
            }
        }

        
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var ret = _serviceCustomer.getCustomer(id);
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
            var customers = _serviceCustomer.getAllCustomers();
            return Ok(customers);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Customer customer)
        {
            var ret = _serviceCustomer.update(id, customer);
            if(ret == true)
            {
                return Ok(Get(id));
            }
            else
            {
                return BadRequest(new {message = "[Controller-Customer] - Fail to trying to Update Custumer."});
            }
            
        }

    }
}