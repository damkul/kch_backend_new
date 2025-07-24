namespace kch_backend.Application.DTOs.Branch
{
    public class BranchDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Location { get; set; }
        public string? Contact { get; set; }
        public string? ManagerName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
