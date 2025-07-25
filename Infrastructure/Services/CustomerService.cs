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

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            try
            {
                Log.Information("Fetching all customers");

                var customers = await _context.Customers
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

                Log.Information("Fetched {Count} customers", customers.Count);
                return customers;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while fetching customer list");
                throw;
            }
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            try
            {
                Log.Information("Fetching customer by ID: {Id}", id);
                var customer = await _context.Customers.FindAsync(id);

                if (customer == null)
                {
                    Log.Warning("Customer not found with ID: {Id}", id);
                    return null;
                }

                return new CustomerDto
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
            catch (Exception ex)
            {
                Log.Error(ex, "Error while fetching customer with ID: {Id}", id);
                throw;
            }
        }

        public async Task<CustomerDto> AddAsync(CustomerDto dto)
        {
            try
            {
                Log.Information("Adding new customer: {Name}", dto.Name);

                var customer = new Customer
                {
                    BranchId = dto.BranchId,
                    Name = dto.Name,
                    Contact = dto.Contact,
                    Email = dto.Email,
                    Aadhaar = dto.Aadhaar,
                    Address = dto.Address,
                    CreatedOn = DateTime.UtcNow
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                dto.Id = customer.Id;
                dto.CreatedOn = (DateTime)customer.CreatedOn;

                Log.Information("Customer added successfully with ID: {Id}", dto.Id);
                return dto;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while adding customer: {Name}", dto.Name);
                throw;
            }
        }

        public async Task<CustomerDto?> UpdateAsync(int id, CustomerDto dto)
        {
            try
            {
                Log.Information("Updating customer with ID: {Id}", id);
                var customer = await _context.Customers.FindAsync(id);

                if (customer == null)
                {
                    Log.Warning("Customer not found for update with ID: {Id}", id);
                    return null;
                }

                customer.Name = dto.Name;
                customer.Contact = dto.Contact;
                customer.Email = dto.Email;
                customer.Aadhaar = dto.Aadhaar;
                customer.Address = dto.Address;
                customer.BranchId = dto.BranchId;

                await _context.SaveChangesAsync();

                Log.Information("Customer updated successfully with ID: {Id}", id);
                return dto;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while updating customer with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Log.Information("Deleting customer with ID: {Id}", id);
                var customer = await _context.Customers.FindAsync(id);

                if (customer == null)
                {
                    Log.Warning("Customer not found for deletion with ID: {Id}", id);
                    return false;
                }

                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();

                Log.Information("Customer deleted successfully with ID: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while deleting customer with ID: {Id}", id);
                throw;
            }
        }
    }
}
