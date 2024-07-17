using MainWindow.Validation;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MainWindow.Payment
{
    /// <summary>
    /// Interaction logic for CreatePayment.xaml
    /// </summary>
    public partial class CreatePayment : Window
    {
        private readonly IPaymentService paymentService;
        private readonly IMemberDetailService memberDetailService;
        private readonly IMembershipService membershipService;
        private readonly IUserService userService;

        public CreatePayment()
        {
            InitializeComponent();
            paymentService = new PaymentService();
            memberDetailService = new MemberDetailService();    
            membershipService = new MembershipService();
            userService = new UserService();

            LoadMembers();
            LoadPaymentMethod();
        }

        private void btnCreatePayment_Click(object sender, RoutedEventArgs e)
        {
            ValidationInput validationInput = new ValidationInput();
            var paymentMethod = cboPaymentMethod.SelectedValue;
            if(paymentMethod == null)
            {
                MessageBox.Show("Please select payment method", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var memberId = cboMember.SelectedValue;
            if(memberId == null)
            {

                MessageBox.Show("Please select member", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var amount = txtAmount.Text;

            var payment = new BusinessObjects.Models.Payment
            {
                MemberId = (int)memberId,
                PaymentMethod = paymentMethod.ToString(),
                Amount = decimal.Parse(amount),
                PaymentDate = DateTime.Now
            };

            paymentService.AddPayment(payment);
            MessageBox.Show("Payment created successfully", "Notification!");
        
        }

        private void LoadMembers()
        {
            var list  = memberDetailService.GetMemberDetails();
            cboMember.ItemsSource = list;
            cboMember.DisplayMemberPath = "MemberId";
            cboMember.SelectedValuePath = "MemberId";
        }

        private void LoadPaymentMethod()
        {
            var list = new List<string> { "Cash", "Credit Card", "Debit Card" };
            cboPaymentMethod.ItemsSource = list;
        }

        private void cboMember_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var memberId = cboMember.SelectedValue;

            var member = memberDetailService.GetMemberDetail((int)memberId);

            var memberShip = membershipService.GetMembership((int)member.MembershipId);

            var amount = memberShip.Price;

            txtAmount.Text = amount.ToString();

            var user = userService.GetUserByUserId(member.UserId);

            txtMemberName.Text = user.FullName;

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
