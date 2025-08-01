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

        public async Task<List<VendorPaymentDto>> GetAllPaymentsAsync(int? eventId)
        {
            try
            {
                Log.Information("Fetching vendor payments with optional EventId filter: {EventId}", eventId);

                var query = _context.Vendorpayments.AsQueryable();

                if (eventId.HasValue)
                {
                    query = query.Where(p => p.EventVendor.EventId == eventId.Value);
                }

                var result = await query
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

                Log.Information("Fetched {Count} vendor payments", result.Count);
                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching vendor payments");
                throw;
            }
        }

        public async Task<bool> UpdatePaymentAsync(VendorPaymentUpdateRequest request)
        {
            try
            {
                Log.Information("Updating vendor payment with ID: {Id}", request.Id);

                var payment = await _context.Vendorpayments
                    .Include(p => p.EventVendor)
                    .FirstOrDefaultAsync(p => p.Id == request.Id);

                if (payment == null)
                {
                    Log.Warning("Vendor payment not found with ID: {Id}", request.Id);
                    return false;
                }

                // Optional EventId filter check
                if (request.EventId.HasValue && payment.EventVendor.EventId != request.EventId.Value)
                {
                    Log.Warning("Vendor payment ID: {Id} does not match EventId: {EventId}", request.Id, request.EventId);
                    return false;
                }

                payment.EventVendorId = request.EventVendorId;
                payment.PaymentDate = request.PaymentDate;
                payment.AmountPaid = request.AmountPaid;
                payment.PaymentMode = request.PaymentMode;
                payment.Remarks = request.Remarks;

                await _context.SaveChangesAsync();

                Log.Information("Vendor payment updated successfully with ID: {Id}", request.Id);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error updating vendor payment with ID: {Id}", request.Id);
                throw;
            }
        }


    }
}
