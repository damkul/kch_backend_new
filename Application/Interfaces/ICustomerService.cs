using kch_backend.Application.DTOs.Customer;

namespace kch_backend.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllAsync();
        Task<CustomerDto?> GetByIdAsync(int id);
        Task<CustomerDto> AddAsync(CustomerDto dto);
        Task<CustomerDto?> UpdateAsync(int id, CustomerDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
