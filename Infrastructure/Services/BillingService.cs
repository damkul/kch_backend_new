using kch_backend.Application.DTOs.Bill;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using kch_backend.Entities;
using Microsoft.EntityFrameworkCore;


namespace kch_backend.Infrastructure.Services
{
    public class BillingService : IBillingService
    {
        private readonly KchDbContext _context;

        public BillingService(KchDbContext context)
        {
            _context = context;
        }

        public async Task<BillDto> GenerateBillAsync(int eventId)
        {
            var ev = await _context.events
                .Include(e => e.eventfacilities)
                .ThenInclude(f => f.Facility)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (ev == null) throw new Exception("Event not found.");

            // Sample calculation logic
            decimal hallCharge = 10000; // fixed or pulled from package
            decimal facilityCharge = (decimal)ev.eventfacilities.Sum(f => f.Total);
            decimal cateringCharge = 0;  // implement later
            decimal decorationCharge = 0; // implement later
            decimal tax = (hallCharge + facilityCharge + cateringCharge) * 0.18M;
            decimal discount = 0;
            decimal total = hallCharge + facilityCharge + cateringCharge + decorationCharge + tax - discount;

            var bill = new Bill
            {
                EventId = eventId,
                HallCharge = hallCharge,
                FacilityCharge = facilityCharge,
                CateringCharge = cateringCharge,
                DecorationCharge = decorationCharge,
                Tax = tax,
                Discount = discount,
                TotalAmount = total,
                CreatedOn = DateTime.UtcNow
            };

            _context.bills.Add(bill);
            await _context.SaveChangesAsync();

            return new BillDto
            {
                Id = bill.Id,
                EventId = bill.EventId,
                HallCharge = (decimal)bill.HallCharge,
                FacilityCharge = (decimal)bill.FacilityCharge,
                CateringCharge = (decimal)bill.CateringCharge,
                DecorationCharge = (decimal)bill.DecorationCharge,
                Tax = (decimal)bill.Tax,
                Discount = (decimal)bill.Discount,
                TotalAmount = (decimal)bill.TotalAmount,
                CreatedOn = (DateTime)bill.CreatedOn
            };
        }

        public async Task<BillDto?> GetBillByEventIdAsync(int eventId)
        {
            var bill = await _context.bills.FirstOrDefaultAsync(b => b.EventId == eventId);

            return bill == null ? null : new BillDto
            {
                Id = bill.Id,
                EventId = bill.EventId,
                HallCharge = (decimal)bill.HallCharge,
                FacilityCharge = (decimal)bill.FacilityCharge,
                CateringCharge = (decimal)bill.CateringCharge,
                DecorationCharge = (decimal)bill.DecorationCharge,
                Tax = (decimal)bill.Tax,
                Discount = (decimal)bill.Discount,
                TotalAmount = (decimal)bill.TotalAmount,
                CreatedOn = (DateTime)bill.CreatedOn
            };
        }
    }
}
