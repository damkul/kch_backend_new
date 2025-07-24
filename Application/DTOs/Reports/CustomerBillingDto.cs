namespace kch_backend.Application.DTOs.Reports
{
    public class CustomerBillingDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = "";
        public string Contact { get; set; } = "";
        public int EventId { get; set; }
        public string EventName { get; set; } = "";
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
