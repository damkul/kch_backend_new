namespace kch_backend.Application.DTOs.Reports
{
    public class PaymentSummaryDto
    {
        public string PaymentMode { get; set; } = ""; // Customer or Vendor
        public DateTime PaymentDate { get; set; }
        public decimal Total { get; set; }
    }
}
