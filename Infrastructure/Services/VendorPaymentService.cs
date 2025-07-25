using kch_backend.Application.DTOs.Vendor;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using kch_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace kch_backend.Infrastructure.Services
{
    public class VendorPaymentService : IVendorPaymentService
    {
        private readonly KchDbContext _context;

        public VendorPaymentService(KchDbContext context)
        {
            _context = context;
        }

        public async Task<List<VendorPaymentDto>> GetPaymentsByEventVendorAsync(int eventVendorId)
        {
            try
            {
                Log.Information("Fetching vendor payments for EventVendorId: {EventVendorId}", eventVendorId);

                var result = await _context.Vendorpayments
                    .Where(p => p.EventVendorId == eventVendorId)
                    .OrderByDescending(p => p.PaymentDate)
                    .Select(p => new VendorPaymentDto
                    {
                        Id = p.Id,
                        EventVendorId = p.EventVendorId,
                        PaymentDate = p.PaymentDate,
                        AmountPaid = p.AmountPaid,
                        PaymentMode = p.PaymentMode,
                        Remarks = p.Remarks
                    })
                    .ToListAsync();

                Log.Information("Fetched {Count} vendor payments for EventVendorId: {EventVendorId}", result.Count, eventVendorId);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching vendor payments for EventVendorId: {EventVendorId}", eventVendorId);
                throw;
            }
        }

        public async Task<bool> AddPaymentAsync(VendorPaymentDto dto)
        {
            try
            {
                Log.Information("Adding vendor payment for EventVendorId: {EventVendorId}", dto.EventVendorId);

                var payment = new VendorPayment
                {
                    EventVendorId = dto.EventVendorId,
                    PaymentDate = dto.PaymentDate,
                    AmountPaid = dto.AmountPaid,
                    PaymentMode = dto.PaymentMode,
                    Remarks = dto.Remarks
                };

                _context.Vendorpayments.Add(payment);
                await _context.SaveChangesAsync();

                Log.Information("Vendor payment added successfully for EventVendorId: {EventVendorId}", dto.EventVendorId);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error adding vendor payment for EventVendorId: {EventVendorId}", dto.EventVendorId);
                throw;
            }
        }

        public async Task<bool> DeletePaymentAsync(int id)
        {
            try
            {
                Log.Information("Deleting vendor payment with ID: {Id}", id);

                var payment = await _context.Vendorpayments.FindAsync(id);
                if (payment == null)
                {
                    Log.Warning("Vendor payment not found with ID: {Id}", id);
                    return false;
                }

                _context.Vendorpayments.Remove(payment);
                await _context.SaveChangesAsync();

                Log.Information("Vendor payment deleted successfully with ID: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error deleting vendor payment with ID: {Id}", id);
                throw;
            }
        }
    }
}
