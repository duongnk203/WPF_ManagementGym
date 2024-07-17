using BusinessObjects.Models;
using Repositories;

namespace Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository iPaymentRepository;

        public PaymentService()
        {
            iPaymentRepository = new PaymentRepository();
        }
        public List<Payment> GetPayments()
            => iPaymentRepository.GetPayments();

        public Payment GetPaymentByPaymentId(int paymentId)
            => iPaymentRepository.GetPaymentByPaymentId(paymentId);

        public void AddPayment(Payment payment)
            => iPaymentRepository.AddPayment(payment);

        public List<Payment> GetPaymentsByMemberId(int memberId)
            => iPaymentRepository.GetPaymentsByMemberId(memberId);
    }
}
