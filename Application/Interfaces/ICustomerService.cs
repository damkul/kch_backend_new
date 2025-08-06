using kch_backend.Application.DTOs.Customer;

namespace kch_backend.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> AddAsync(List<CustomerDto> dtos);
        Task<CustomerDto> AddAsync(CustomerDto dto);

        Task<List<CustomerDto>> UpdateAsync(List<CustomerDto> dtos);
        Task<CustomerDto?> UpdateAsync(int id, CustomerDto dto);

        Task<List<CustomerDto>> GetAllAsync();
        Task<CustomerDto?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
