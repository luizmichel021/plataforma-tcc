
using plataformatcc.Models;

namespace plataformatcc.Interfaces{
    public interface ICustomerService
    {
        Customer? create(Customer customer);
        Customer update(Customer customer);
        bool partialUpdate();
        List<Customer> getAllCustomers();
        int delete(int id);
        Customer getCustomer(int id);
    }
}