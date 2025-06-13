
using Model;

namespace Interfaces{
    public interface IService
    {
        Customer create(Customer customer);
        bool update(Guid id, Customer customer);
        List<Customer>? getAllCustomers();
        bool delete(Guid id);
        Customer? getCustomer(Guid id);

        bool partialUpdate(Guid id, string? name, string? surname, string? email, DateTime? birthdate);
    }
}