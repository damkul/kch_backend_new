namespace kch_backend.Application.DTOs.Vendor
{
    public class VendorPaymentUpdateRequest
    {
        public int Id { get; set; }
        public int? EventId { get; set; } // Optional filter for safety
        public int EventVendorId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMode { get; set; }
        public string Remarks { get; set; }
    }
}
