using plataformatcc.Models;

namespace plataformatcc.Interfaces
{
    public interface ICustomerStorage
    {
        Customer create(Customer customer);
        bool update(Guid id,Customer customer);
        bool partialUpdate();
        List<Customer>? getAllCustomers();
        bool delete(Guid id);
        Customer? getCustomer(Guid id);
    }
}