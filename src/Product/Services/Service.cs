
using Models.Product;
using Repositories;

namespace Services
{

    public class Service
    {

        private readonly ILogger<Service> _logger;
        private readonly Repository _repository;

        public Service(ILogger<Service> logger, Repository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public Product Register(Product product)
        {
            var ret = _repository.Register(product);

            if (ret != null)
            {
                return product;
            }
            else
            {
                return null;
            }
        }


        public bool Delete(Guid id)
        {
            var ret = _repository.Delete(id);
            if (ret)
            {
                return ret;
            }
            else
            {
                return ret;
            }
        }

        public bool Update(Guid id, string? name, string? description, float? price, int? quantity)
        {
            var ret = _repository.Update(id, name, description, price, quantity);
            if (ret)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Product> ListAllProducts()
        {
            var ret = _repository.ListAllProducts();
            if (ret != null)
            {
                return ret;
            }
            else
            {
                return null;
            }
        }

        public List<Product> ListProductsFilter(string? name, string? description, float? pricemax, float? pricemin, int? quantity)
        {
            var ret = _repository.ListProductsFilter(name, description, pricemax, pricemin,quantity);
            if (ret != null)
            {
                return ret;
            }
            else
            {
                return null;
            }
        }
    }
}