using plataformatcc.Models;

namespace plataformatcc.Interfaces
{
    public interface ICustomer
    {
        bool create(Customer customer);
        bool update();
        bool partialUpdate();
        List<Customer> listCustomers();
        bool delete(int id);
    }
}