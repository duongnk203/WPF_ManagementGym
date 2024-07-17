using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IPaymentRepository
    {
        List<Payment> GetPayments();
        
        Payment GetPaymentByPaymentId(int paymentId);

        void AddPayment(Payment payment);

        List<Payment> GetPaymentsByMemberId(int memberId);
    }
}
