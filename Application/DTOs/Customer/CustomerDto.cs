namespace kch_backend.Application.DTOs.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public string Name { get; set; }
        public string? Contact { get; set; }
        public string? Email { get; set; }
        public string? Aadhaar { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
