using BusinessObjects.Models;
using DataAccessLayer;

namespace Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        public List<Payment> GetPayments()
            => PaymentDAO.GetPayments();

        public Payment GetPaymentByPaymentId(int paymentId)
            => PaymentDAO.GetPaymentByPaymentId(paymentId);

        public void AddPayment(Payment payment)
            => PaymentDAO.AddPayment(payment);

        public List<Payment> GetPaymentsByMemberId(int memberId)
            => PaymentDAO.GetPaymentsByMemberId(memberId);
    }
}
