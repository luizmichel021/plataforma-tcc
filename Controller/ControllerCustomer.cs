

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
            var retcustomer = _serviceCustomer.create(customer);
            if (retcustomer != null)
            {
                return Ok(retcustomer);
            }
            else
            {
                return BadRequest(new {message = "[Controller-Customer] - Fail to Create Customer."});
            }
        }
    }
}