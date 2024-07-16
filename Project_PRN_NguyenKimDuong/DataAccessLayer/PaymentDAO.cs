using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PaymentDAO
    {
        public static List<Payment> GetPayments()
        {
            using var context = new GymManagementContext();
            var payments = context.Payments.ToList();
            return payments;
        }

        public static Payment GetPaymentByPaymentId(int paymentId)
        {
            using var context = new GymManagementContext();
            var payment = context.Payments.FirstOrDefault(p => p.PaymentId == paymentId);
            return payment;
        }
    }
}
