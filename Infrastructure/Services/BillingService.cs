using kch_backend.Application.DTOs.Bill;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using kch_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
            try
            {
                Log.Information("Generating bill for EventId: {EventId}", eventId);

                var ev = await _context.events
                    .Include(e => e.eventfacilities)
                    .ThenInclude(f => f.Facility)
                    .FirstOrDefaultAsync(e => e.Id == eventId);

                if (ev == null)
                {
                    Log.Warning("Event not found with ID: {EventId}", eventId);
                    throw new Exception("Event not found.");
                }

                // Charges
                decimal hallCharge = 10000; // static for now
                decimal facilityCharge = ev.eventfacilities.Sum(f => f.Total ?? 0);
                decimal cateringCharge = 0;    // placeholder
                decimal decorationCharge = 0;  // placeholder
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

                Log.Information("Bill generated successfully for EventId: {EventId}, BillId: {BillId}", eventId, bill.Id);

                return new BillDto
                {
                    Id = bill.Id,
                    EventId = bill.EventId,
                    HallCharge = bill.HallCharge ?? 0,
                    FacilityCharge = bill.FacilityCharge ?? 0,
                    CateringCharge = bill.CateringCharge ?? 0,
                    DecorationCharge = bill.DecorationCharge ?? 0,
                    Tax = bill.Tax ?? 0,
                    Discount = bill.Discount ?? 0,
                    TotalAmount = bill.TotalAmount ?? 0,
                    CreatedOn = bill.CreatedOn ?? DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error generating bill for EventId: {EventId}", eventId);
                throw;
            }
        }

        public async Task<BillDto?> GetBillByEventIdAsync(int eventId)
        {
            try
            {
                Log.Information("Fetching bill for EventId: {EventId}", eventId);

                var bill = await _context.bills.FirstOrDefaultAsync(b => b.EventId == eventId);

                if (bill == null)
                {
                    Log.Warning("No bill found for EventId: {EventId}", eventId);
                    return null;
                }

                return new BillDto
                {
                    Id = bill.Id,
                    EventId = bill.EventId,
                    HallCharge = bill.HallCharge ?? 0,
                    FacilityCharge = bill.FacilityCharge ?? 0,
                    CateringCharge = bill.CateringCharge ?? 0,
                    DecorationCharge = bill.DecorationCharge ?? 0,
                    Tax = bill.Tax ?? 0,
                    Discount = bill.Discount ?? 0,
                    TotalAmount = bill.TotalAmount ?? 0,
                    CreatedOn = bill.CreatedOn ?? DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching bill for EventId: {EventId}", eventId);
                throw;
            }
        }
    }
}
