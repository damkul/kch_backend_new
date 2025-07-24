namespace kch_backend.Application.DTOs.Vendor
{
    public class VendorPaymentDto
    {
        public int Id { get; set; }
        public int EventVendorId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string? PaymentMode { get; set; }
        public string? Remarks { get; set; }
    }
}
