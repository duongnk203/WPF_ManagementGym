using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPaymentService
    {
        List<Payment> GetPayments();

        Payment GetPaymentByPaymentId(int paymentId);

        void AddPayment(Payment payment);

        List<Payment> GetPaymentsByMemberId(int memberId);
    }
}
