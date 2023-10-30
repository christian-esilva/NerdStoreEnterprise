using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;
using NSE.Customers.API.Models;
using NSE.Customers.API.Models.Contracts;

namespace NSE.Customers.API.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;
        
        public CustomerRepository(CustomerContext context)
        {
            _context = context;
        }

        public IUnityOfWork UnityOfWork => _context;

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<Customer> GetByCpf(string cpf)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
        }

        public async Task<Customer> GetById(Guid id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public void Dispose() => _context?.Dispose();
    }
}
