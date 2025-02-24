using plataformatcc.Models;

namespace plataformatcc.Interfaces
{
    public interface ICustomerStorage
    {
        Customer? create(Customer customer);
        bool update(int id,Customer customer);
        bool partialUpdate();
        List<Customer>? getAllCustomers();
        bool delete(int id);
        Customer? getCustomer(int id);
    }
}