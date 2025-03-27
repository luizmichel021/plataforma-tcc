using Models.Product;

namespace plataformatcc.Interfaces
{
    public interface IProductRepository
    {
        Product Register(Product product);
        bool Update(Guid id,string? name, string? description, float? price, int? quantity);
        bool Delete(Guid id);
        List<Product> ListAllProducts();
        List<Product> ListProducts(string? name, string?description, float? pricemax, float? pricemin, int? quantity);   


    }
}