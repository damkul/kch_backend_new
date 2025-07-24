using kch_backend.Application.DTOs.Vendor;
using kch_backend.Application.Interfaces;
using kch_backend.Data;
using kch_backend.Entities;

namespace kch_backend.Infrastructure.Services
{
    public class VendorPaymentService : IVendorPaymentService
    {
        /* private readonly KchDbContext _context;

         public VendorPaymentService(KchDbContext context)
         {
             _context = context;
         }

         public async Task<List<VendorPaymentDto>> GetPaymentsByEventVendorAsync(int eventVendorId)
         {
             return await _context.VendorPayments
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
         }

         public async Task<bool> AddPaymentAsync(VendorPaymentDto dto)
         {
             var payment = new VendorPayment
             {
                 EventVendorId = dto.EventVendorId,
                 PaymentDate = dto.PaymentDate,
                 AmountPaid = dto.AmountPaid,
                 PaymentMode = dto.PaymentMode,
                 Remarks = dto.Remarks
             };

             _context.VendorPayments.Add(payment);
             await _context.SaveChangesAsync();
             return true;
         }

         public async Task<bool> DeletePaymentAsync(int id)
         {
             var payment = await _context.VendorPayments.FindAsync(id);
             if (payment == null) return false;

             _context.VendorPayments.Remove(payment);
             await _context.SaveChangesAsync();
             return true;
         }*/
        public Task<bool> AddPaymentAsync(VendorPaymentDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePaymentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<VendorPaymentDto>> GetPaymentsByEventVendorAsync(int eventVendorId)
        {
            throw new NotImplementedException();
        }
    }
}
