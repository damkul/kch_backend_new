using kch_backend.Application.DTOs.Customer;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using kch_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace kch_backend.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly KchDbContext _context;

        public CustomerService(KchDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerDto>> AddAsync(List<CustomerDto> dtos)
        {
            Log.Information("Adding {Count} customers", dtos.Count);

            // ✅ Ensure all customers in batch share same EventId if one is provided
            int? eventId = dtos.FirstOrDefault()?.EventId;
            if (eventId.HasValue)
            {
                foreach (var dto in dtos)
                {
                    dto.EventId = eventId;
                }
            }

            var customers = dtos.Select(dto => new Customer
            {
                BranchId = dto.BranchId,
                EventId = dto.EventId,
                Name = dto.Name,
                Contact = dto.Contact,
                Email = dto.Email,
                Aadhaar = dto.Aadhaar,
                Address = dto.Address,
                CreatedOn = DateTime.UtcNow
            }).ToList();

            _context.Customers.AddRange(customers);
            await _context.SaveChangesAsync();

            for (int i = 0; i < customers.Count; i++)
            {
                dtos[i].Id = customers[i].Id;
                dtos[i].CreatedOn = (DateTime)customers[i].CreatedOn;
            }

            return dtos;
        }

        public async Task<CustomerDto> AddAsync(CustomerDto dto)
        {
            var result = await AddAsync(new List<CustomerDto> { dto });
            return result.First();
        }

        public async Task<List<CustomerDto>> UpdateAsync(List<CustomerDto> dtos)
        {
            Log.Information("Updating {Count} customers", dtos.Count);

            // ✅ If batch update includes EventId, apply it to all customers
            int? eventId = dtos.FirstOrDefault()?.EventId;
            if (eventId.HasValue)
            {
                foreach (var dto in dtos)
                {
                    dto.EventId = eventId;
                }
            }

            foreach (var dto in dtos)
            {
                var customer = await _context.Customers.FindAsync(dto.Id);
                if (customer != null)
                {
                    customer.Name = dto.Name;
                    customer.Contact = dto.Contact;
                    customer.Email = dto.Email;
                    customer.Aadhaar = dto.Aadhaar;
                    customer.Address = dto.Address;
                    customer.BranchId = dto.BranchId;
                    customer.EventId = dto.EventId;
                }
                else
                {
                    Log.Warning("Customer with ID {Id} not found for update", dto.Id);
                }
            }

            await _context.SaveChangesAsync();
            return dtos;
        }

        public async Task<CustomerDto?> UpdateAsync(int id, CustomerDto dto)
        {
            dto.Id = id;
            var result = await UpdateAsync(new List<CustomerDto> { dto });
            return result.FirstOrDefault();
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            return await _context.Customers
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    BranchId = (int)c.BranchId,
                    EventId = c.EventId, // ✅ Now included in DTO
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
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return null;

            return new CustomerDto
            {
                Id = customer.Id,
                BranchId = (int)customer.BranchId,
                EventId = customer.EventId, // ✅ Now included in DTO
                Name = customer.Name,
                Contact = customer.Contact,
                Email = customer.Email,
                Aadhaar = customer.Aadhaar,
                Address = customer.Address,
                CreatedOn = (DateTime)customer.CreatedOn
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
