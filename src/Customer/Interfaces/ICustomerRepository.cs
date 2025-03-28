using plataformatcc.Models;

namespace plataformatcc.Interfaces
{
    public interface ICustomerRepository
    {
        Customer create(Customer customer);
        bool update(Guid id,Customer customer);
        bool partialUpdate(Guid id, string? name, string? surname, string? email, DateTime? birthdate);
        List<Customer>? getAllCustomers();
        bool delete(Guid id);
        Customer? getCustomer(Guid id);
    }
}