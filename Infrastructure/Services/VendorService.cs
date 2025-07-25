using kch_backend.Application.DTOs.Vendor;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using kch_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace kch_backend.Infrastructure.Services
{
    public class VendorService : IVendorService
    {
        private readonly KchDbContext _context;

        public VendorService(KchDbContext context)
        {
            _context = context;
        }

        public async Task<List<VendorCategoryDto>> GetCategoriesAsync()
        {
            try
            {
                Log.Information("Fetching all vendor categories");

                var result = await _context.Vendorcategories
                    .Select(c => new VendorCategoryDto { Id = c.Id, Name = c.Name })
                    .ToListAsync();

                Log.Information("Fetched {Count} vendor categories", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching vendor categories");
                throw;
            }
        }

        public async Task<List<VendorDto>> GetAllVendorsAsync()
        {
            try
            {
                Log.Information("Fetching all vendors");

                var result = await _context.Vendors
                    .Select(v => new VendorDto
                    {
                        Id = v.Id,
                        Name = v.Name,
                        CategoryId = v.CategoryId,
                        ContactPerson = v.ContactPerson,
                        Phone = v.Phone,
                        Email = v.Email,
                        Address = v.Address,
                        GstNumber = v.GstNumber
                    })
                    .ToListAsync();

                Log.Information("Fetched {Count} vendors", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching all vendors");
                throw;
            }
        }

        public async Task<VendorDto?> GetVendorByIdAsync(int id)
        {
            try
            {
                Log.Information("Fetching vendor with ID: {Id}", id);

                var v = await _context.Vendors.FindAsync(id);
                if (v == null)
                {
                    Log.Warning("Vendor not found with ID: {Id}", id);
                    return null;
                }

                return new VendorDto
                {
                    Id = v.Id,
                    Name = v.Name,
                    CategoryId = v.CategoryId,
                    ContactPerson = v.ContactPerson,
                    Phone = v.Phone,
                    Email = v.Email,
                    Address = v.Address,
                    GstNumber = v.GstNumber
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching vendor with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> AddOrUpdateVendorAsync(VendorDto dto)
        {
            try
            {
                Log.Information("Saving vendor: {@Dto}", dto);

                Vendor? vendor = dto.Id == 0
                    ? new Vendor()
                    : await _context.Vendors.FindAsync(dto.Id);

                if (vendor == null)
                {
                    Log.Warning("Vendor not found with ID: {Id} for update", dto.Id);
                    return false;
                }

                vendor.Name = dto.Name;
                vendor.CategoryId = dto.CategoryId;
                vendor.ContactPerson = dto.ContactPerson;
                vendor.Phone = dto.Phone;
                vendor.Email = dto.Email;
                vendor.Address = dto.Address;
                vendor.GstNumber = dto.GstNumber;

                if (dto.Id == 0)
                {
                    _context.Vendors.Add(vendor);
                    Log.Information("New vendor added: {Name}", dto.Name);
                }

                await _context.SaveChangesAsync();
                Log.Information("Vendor saved successfully with ID: {Id}", vendor.Id);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error saving vendor: {@Dto}", dto);
                throw;
            }
        }

        public async Task<bool> DeleteVendorAsync(int id)
        {
            try
            {
                Log.Information("Deleting vendor with ID: {Id}", id);

                var vendor = await _context.Vendors.FindAsync(id);
                if (vendor == null)
                {
                    Log.Warning("Vendor not found with ID: {Id}", id);
                    return false;
                }

                _context.Vendors.Remove(vendor);
                await _context.SaveChangesAsync();

                Log.Information("Vendor deleted with ID: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error deleting vendor with ID: {Id}", id);
                throw;
            }
        }
    }
}
