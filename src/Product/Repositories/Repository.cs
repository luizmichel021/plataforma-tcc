using Interfaces;
using Models.Product;

namespace Repositories
{
    public class Repository : IRepository
    {
        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Product> ListAllProducts()
        {
            throw new NotImplementedException();
        }

        public List<Product> ListProducts(string? name, string? description, float? pricemax, float? pricemin, int? quantity)
        {
            throw new NotImplementedException();
        }

        public Product Register(Product product)
        {
            throw new NotImplementedException();
        }

        public bool Update(Guid id, string? name, string? description, float? price, int? quantity)
        {
            throw new NotImplementedException();
        }
    }
}