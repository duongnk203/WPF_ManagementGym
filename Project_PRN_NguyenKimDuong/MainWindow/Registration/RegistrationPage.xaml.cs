using DataAccessLayer;
using MainWindow.ClassRegistration;
using MainWindow.Payment;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainWindow.Registration
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private readonly IClassService classService;
        private readonly IMemberDetailService memberDetailService;
        private readonly IPaymentService paymentService;
        private readonly IClassRegistrationService classRegistrationService;
        private BusinessObjects.Models.ClassRegistration classRegistration;

        public RegistrationPage()
        {
            InitializeComponent();

            classService = new ClassService();
            memberDetailService = new MemberDetailService();
            paymentService = new PaymentService();
            classRegistrationService = new ClassRegistrationService();

            LoadClass();
            LoadMember();
            LoadPaymentMethod();
            LoadListPayment();
            LoadListClassRegistration();
            LoadSearchRegistration();
        }

        private void LoadClass()
        {
            cboClass.Items.Clear();
            var classes = classService.GetClasses();
            cboClass.ItemsSource = classes;
            cboClass.DisplayMemberPath = "ClassName";
            cboClass.SelectedValuePath = "ClassId";
        }

        private void LoadMember()
        {
            var memberDetails = memberDetailService.GetMemberDetails();
            cboMemberId.Items.Clear();
            cboMemberId.ItemsSource = memberDetails;
            cboMemberId.DisplayMemberPath = "MemberId";
            cboMemberId.SelectedValuePath = "MemberId";
            cboMember.Items.Clear();
            cboMember.ItemsSource = memberDetails;
            cboMember.DisplayMemberPath = "MemberId";
            cboMember.SelectedValuePath = "MemberId";
        }

        private void LoadPaymentMethod()
        {
            cboPaymentMethod.Items.Clear();
            var listPaymentMethod = new List<string> { "Cash", "Credit Card", "Debit Card" };
            cboPaymentMethod.ItemsSource = listPaymentMethod;

        }

        private void LoadListPayment()
        {
            var listPaymnents = paymentService.GetPayments();
            dgListPayment.ItemsSource = listPaymnents;
        }

        private void LoadListClassRegistration()
        {
            var listClassRegistrations = classRegistrationService.GetClassRegistrations();
            dgListClassRegistration.ItemsSource = listClassRegistrations;
        }

        private void dgListClassRegistration_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid != null && dataGrid.SelectedIndex >= 0 && dataGrid.SelectedIndex < dataGrid.Items.Count)
            {
                DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;
                if (row != null)
                {
                    DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row)?.Parent as DataGridCell;
                    if (RowColumn != null)
                    {
                        string id = ((TextBlock)RowColumn.Content)?.Text;
                        if (!string.IsNullOrEmpty(id))
                        {
                            if (int.TryParse(id, out int classRegistrationID))
                            {
                                classRegistration = classRegistrationService.GetClassRegistrationByClassRegistrationId(classRegistrationID);
                                if (classRegistration != null)
                                {
                                    LoadClassRegisInfo(classRegistration);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void LoadClassRegisInfo(BusinessObjects.Models.ClassRegistration classRegistration)
        {
            txtRegistrationId.Text = classRegistration.RegistrationId.ToString();
            cboClass.SelectedValue = classRegistration.ClassId;
            cboMember.SelectedValue = classRegistration.MemberId;
            dpRegegistraionDate.SelectedDate = classRegistration.RegistrationDate;
        }

        private void btnCreateClassRegistration_Click(object sender, RoutedEventArgs e)
        {
            CreateClassRegistration createClassRegistration = new CreateClassRegistration();
            createClassRegistration.ShowDialog();
            LoadListClassRegistration();
        }

        public void LoadPaymentInfo(BusinessObjects.Models.Payment payment)
        {
            txtPaymentId.Text = payment.PaymentId.ToString();
            txtAmount.Text = payment.Amount.ToString();
            cboPaymentMethod.SelectedValue = payment.PaymentMethod;
            dpPaymentDate.Text = payment.PaymentDate.ToString();
        }

        private void dgListPayment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid != null && dataGrid.SelectedIndex >= 0 && dataGrid.SelectedIndex < dataGrid.Items.Count)
            {
                DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;
                if (row != null)
                {
                    DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row)?.Parent as DataGridCell;
                    if (RowColumn != null)
                    {
                        string id = ((TextBlock)RowColumn.Content)?.Text;
                        if (!string.IsNullOrEmpty(id))
                        {
                            if (int.TryParse(id, out int paymentId))
                            {
                                BusinessObjects.Models.Payment payment = paymentService.GetPaymentByPaymentId(paymentId);
                                if (payment != null)
                                {
                                    LoadPaymentInfo(payment);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnUpdateCreateRegistration_Click(object sender, RoutedEventArgs e)
        {
            var registrationId = txtRegistrationId.Text;
            if(registrationId == "")
            {
                MessageBox.Show("Please select a registration to update", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ValidationInput validation = new ValidationInput();
            var classId = cboClass.SelectedValue;
            if (classId == null)
            {
                MessageBox.Show("Please select class", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var memberId = cboMember.SelectedValue;
            if (memberId == null)
            {
                MessageBox.Show("Please select member", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var registrationDate = dpRegegistraionDate.SelectedDate;
            if (registrationDate == null)
            {
                MessageBox.Show("Please select registration date", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var classRegistration = new BusinessObjects.Models.ClassRegistration
            {
                RegistrationId = int.Parse(registrationId),
                ClassId = (int)classId,
                MemberId = (int)memberId,
                RegistrationDate = (DateTime)registrationDate
            };
            try
            {
                classRegistrationService.UpdateClassRegistration(classRegistration);
                MessageBox.Show("Update class registration successfully", "Notification!");
                LoadListClassRegistration();
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteClassRegistration_Click(object sender, RoutedEventArgs e)
        {
            var registrationId = txtRegistrationId.Text;
            if (registrationId == "")
            {
                MessageBox.Show("Please select a registration to update", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (MessageBox.Show("Are you sure you want to delete this class registration?", "Notification!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    classRegistrationService.DeleteClassRegistration(int.Parse(registrationId));
                    MessageBox.Show("Delete class registration successfully", "Notification!");
                    LoadListClassRegistration();
                }
                catch (DataAccessException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnCreatePayment_Click(object sender, RoutedEventArgs e)
        {
            CreatePayment createPayment = new CreatePayment();
            createPayment.ShowDialog();
            LoadListPayment();
        }

        private void btnSearchRegsitration_Click(object sender, RoutedEventArgs e)
        {
            ValidationInput validationInput = new ValidationInput();
            var selectedSearch = cboSearchRegistration.SelectedValue;
            if (selectedSearch == null)
            {
                MessageBox.Show("Please select search type", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var searchValue = txtSearchRegistration.Text;
            if(searchValue == "")
            {
                MessageBox.Show("Please enter search value", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(!validationInput.InputNumber(searchValue))
            {
                return;
            }

            if(selectedSearch.ToString() == "MemberId")
            {
                var classRegistrations = classRegistrationService.GetClassRegistrationsByMemberId(int.Parse(searchValue));
                dgListClassRegistration.ItemsSource = classRegistrations;
            }
            else if(selectedSearch.ToString() == "ClassId")
            {
                var classRegistrations = classRegistrationService.GetClassRegistrationsByClassId(int.Parse(searchValue));
                dgListClassRegistration.ItemsSource = classRegistrations;
            }
        }

        private void LoadSearchRegistration()
        {
            List<string> list = ["MemberId", "ClassId"];
            cboSearchRegistration.Items.Clear();
            cboSearchRegistration.ItemsSource = list;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtSearchRegistration.Text = "";
            LoadListClassRegistration();
        }

        private void btnResetPayment_Click(object sender, RoutedEventArgs e)
        {
            txtSearchMemberId.Text = "";
            LoadListPayment();
        }

        private void btnSearchPayment_Click(object sender, RoutedEventArgs e)
        {
            var memberId = txtSearchMemberId.Text;
            if(memberId == "")
            {
                MessageBox.Show("Please enter member id", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(!new ValidationInput().InputNumber(memberId))
            {
                return;
            }
            var payments = paymentService.GetPaymentsByMemberId(int.Parse(memberId));
            dgListPayment.ItemsSource = payments;
        }
    }
}
