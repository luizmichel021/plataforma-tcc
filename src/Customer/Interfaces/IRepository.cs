using Model;

namespace Interfaces
{
    public interface IRepository
    {
        Customer create(Customer customer);
        bool update(Guid id,Customer customer);
        bool partialUpdate(Guid id, string? name, string? surname, string? email, DateTime? birthdate);
        List<Customer>? getAllCustomers();
        bool delete(Guid id);
        Customer? getCustomer(Guid id);
    }
}