
using plataformatcc.Models;

namespace plataformatcc.Interfaces{
    public interface ICustomerService
    {
        Customer? create(Customer customer);
        bool update(int id, Customer customer);
        List<Customer>? getAllCustomers();
        int delete(int id);
        Customer? getCustomer(int id);
    }
}