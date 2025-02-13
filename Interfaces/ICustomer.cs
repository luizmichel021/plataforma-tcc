using plataformatcc.Models;

namespace plataformatcc.Interfaces
{
    public interface ICustomer
    {
        bool create(Customer customer);

        bool update();

        bool partialUpdate();

        bool delete(int id);

        List<Customer> listCustomers();
    }
}