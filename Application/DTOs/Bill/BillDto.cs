namespace kch_backend.Application.DTOs.Bill
{
    public class BillDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public decimal HallCharge { get; set; }
        public decimal FacilityCharge { get; set; }
        public decimal CateringCharge { get; set; }
        public decimal DecorationCharge { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
