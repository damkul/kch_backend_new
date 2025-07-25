using kch_backend.Application.DTOs.Customer;
using kch_backend.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace kch_backend.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        /* private readonly KchDbContext _context;

         public CustomerService(KchDbContext context)
         {
             _context = context;
         }

         public async Task<List<CustomerDto>> GetAllAsync()
         {
             return await _context.customers
                 .Select(c => new CustomerDto
                 {
                     Id = c.Id,
                     BranchId = (int)c.BranchId,
                     Name = c.Name,
                     Contact = c.Contact,
                     Email = c.Email,
                     Aadhaar = c.Aadhaar,
                     Address = c.Address,
                     CreatedOn = (DateTime)c.CreatedOn
                 })
                 .ToListAsync();
         }

         public async Task<CustomerDto?> GetByIdAsync(int id)
         {
             var customer = await _context.customers.FindAsync(id);
             return customer == null ? null : new CustomerDto
             {
                 Id = customer.Id,
                 BranchId = (int)customer.BranchId,
                 Name = customer.Name,
                 Contact = customer.Contact,
                 Email = customer.Email,
                 Aadhaar = customer.Aadhaar,
                 Address = customer.Address,
                 CreatedOn = (DateTime)customer.CreatedOn
             };
         }

         public async Task<CustomerDto> AddAsync(CustomerDto dto)
         {
             var customer = new customer
             {
                 BranchId = dto.BranchId,
                 Name = dto.Name,
                 Contact = dto.Contact,
                 Email = dto.Email,
                 Aadhaar = dto.Aadhaar,
                 Address = dto.Address,
                 CreatedOn = DateTime.UtcNow
             };
             _context.customers.Add(customer);
             await _context.SaveChangesAsync();

             dto.Id = customer.Id;
             dto.CreatedOn = (DateTime)customer.CreatedOn;
             return dto;
         }

         public async Task<CustomerDto?> UpdateAsync(int id, CustomerDto dto)
         {
             var customer = await _context.customers.FindAsync(id);
             if (customer == null) return null;

             customer.Name = dto.Name;
             customer.Contact = dto.Contact;
             customer.Email = dto.Email;
             customer.Aadhaar = dto.Aadhaar;
             //customer.Address = dto.Address;
             customer.BranchId = dto.BranchId;

             await _context.SaveChangesAsync();

             return dto;
         }

         public async Task<bool> DeleteAsync(int id)
         {
             var customer = await _context.customers.FindAsync(id);
             if (customer == null) return false;

             _context.customers.Remove(customer);
             await _context.SaveChangesAsync();
             return true;
         }
 */
        public Task<CustomerDto> AddAsync(CustomerDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CustomerDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDto?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDto?> UpdateAsync(int id, CustomerDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
