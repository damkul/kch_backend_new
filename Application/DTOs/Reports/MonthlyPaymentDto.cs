namespace kch_backend.Application.DTOs.Reports
{
    public class MonthlyPaymentDto
    {
        public string PaymentType { get; set; } = "";
        public string Month { get; set; } = ""; // YYYY-MM
        public decimal Total { get; set; }
    }
}
