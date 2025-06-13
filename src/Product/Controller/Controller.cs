using Microsoft.AspNetCore.Mvc;
using Services;
using Models;
using Models.Product;
using Dto.ProductDTO;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Controller
{
    [ApiController]
    [Route("product")]
    public class Controller : ControllerBase
    {
        private readonly Service _service;

        public Controller(Service service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult Resgister(Product product)
        {
            var ret = _service.Register(product);
            if (ret != null)
            {
                return Ok(ret);
            }
            else
            {
                return BadRequest(new { message = "[Controller-Customer] - Fail to Create Customer." });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var ret = _service.Delete(id);
            if (ret)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest(new { message = "[Controller-Customer] - Fail to delete Customer." });
            }
        }

        [HttpPatch("{id}")]
        public ActionResult Update(Guid id, [FromBody] ProductDto dto)
        {

            var ret = _service.Update(id, dto.Name, dto.Description, dto.Price, dto.Quantity);
            if (ret)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(new { message = "[Controller-Customer] - Fail to trying to Update Customer." });
            }

        }

        [HttpGet("all")]
        public ActionResult ListAllProducts()
        {

            var ret = _service.ListAllProducts();
            if (ret != null)
            {
                return Ok(ret);
            }
            else
            {
                return BadRequest(new { message = "[Controller-Customer] - Fail to trying to Update Customer." });
            }

        }

        [HttpGet("filter")]
        public ActionResult ListProductsFilter(string? name, string? description, float? pricemax, float? pricemin, int? quantity)
        {

            var ret = _service.ListProductsFilter(name,description,pricemax,pricemin,quantity);
            if (ret != null)
            {
                return Ok(ret);
            }
            else
            {
                return BadRequest(new { message = "[Controller-Customer] - Fail to trying to Update Customer." });
            }

        }
    }
}