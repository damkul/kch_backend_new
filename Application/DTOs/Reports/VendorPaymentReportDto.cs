namespace kch_backend.Application.DTOs.Reports
{
    public class VendorPaymentReportDto
    {
        public int VendorId { get; set; }
        public string VendorName { get; set; } = "";
        public string Category { get; set; } = "";
        public int EventVendorId { get; set; }
        public int EventId { get; set; }
        public string ServiceDescription { get; set; } = "";
        public decimal TotalDue { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Balance { get; set; }
    }
}
