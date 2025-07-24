using kch_backend.Application.DTOs.Vendor;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using kch_backend.Entities;

namespace kch_backend.Infrastructure.Services
{
    public class VendorService : IVendorService
    {
        /*private readonly KchDbContext _context;

        public VendorService(KchDbContext context)
        {
            _context = context;
        }

        public async Task<List<VendorCategoryDto>> GetCategoriesAsync()
        {
            return await _context.VendorCategories
                .Select(c => new VendorCategoryDto { Id = c.Id, Name = c.Name })
                .ToListAsync();
        }

        public async Task<List<VendorDto>> GetAllVendorsAsync()
        {
            return await _context.Vendors
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
        }

        public async Task<VendorDto?> GetVendorByIdAsync(int id)
        {
            var v = await _context.Vendors.FindAsync(id);
            if (v == null) return null;

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

        public async Task<bool> AddOrUpdateVendorAsync(VendorDto dto)
        {
            Vendor? vendor = dto.Id == 0
                ? new Vendor()
                : await _context.Vendors.FindAsync(dto.Id);

            if (vendor == null) return false;

            vendor.Name = dto.Name;
            vendor.CategoryId = dto.CategoryId;
            vendor.ContactPerson = dto.ContactPerson;
            vendor.Phone = dto.Phone;
            vendor.Email = dto.Email;
            vendor.Address = dto.Address;
            vendor.GstNumber = dto.GstNumber;

            if (dto.Id == 0)
                _context.Vendors.Add(vendor);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVendorAsync(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null) return false;

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
            return true;
        }*/
        public Task<bool> AddOrUpdateVendorAsync(VendorDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteVendorAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<VendorDto>> GetAllVendorsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<VendorCategoryDto>> GetCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<VendorDto?> GetVendorByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
