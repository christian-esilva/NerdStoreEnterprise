using NSE.Core.Data;

namespace NSE.Customers.API.Models.Contracts
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void Add(Customer customer);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetById(Guid id);
        Task<Customer> GetByCpf(string cpf);
    }
}
