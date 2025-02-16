using plataformatcc.Models;

namespace plataformatcc.Interfaces
{
    public interface ICustomerStorage
    {
        bool create(Customer customer);
        bool update(Customer customer);
        bool partialUpdate();
        List<Customer> getAllCustomers();
        bool delete(int id);
        Customer getCustomer(int id);
    }
}