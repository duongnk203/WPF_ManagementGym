using DataAccessLayer;
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

namespace MainWindow.ClassRegistration
{
    /// <summary>
    /// Interaction logic for CreateClassRegistration.xaml
    /// </summary>
    public partial class CreateClassRegistration : Window
    {
        private IClassService classService;
        private IMemberDetailService memberDetailService;
        private IUserService userService;
        private IClassRegistrationService classRegistrationService;
        public CreateClassRegistration()
        {
            InitializeComponent();
            classService = new ClassService();
            memberDetailService = new MemberDetailService();
            userService = new UserService();
            classRegistrationService = new ClassRegistrationService();

            LoadClassCbo();
            LoadMember();
        }

        private void btnCreateClass_Click(object sender, RoutedEventArgs e)
        {
            ValidationInput validation = new ValidationInput();
            var classId = cboClass.SelectedValue;
            if(classId == null)
            {
                MessageBox.Show("Please select class", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var memberId = cboMember.SelectedValue;
            if(memberId == null)
            {
                MessageBox.Show("Please select member", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var registrationDate = dpRegistrationDate.SelectedDate;
            if(registrationDate == null)
            {
                MessageBox.Show("Please select registration date", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var classRegistration = new BusinessObjects.Models.ClassRegistration
            {
                ClassId = (int)classId,
                MemberId = (int)memberId,
                RegistrationDate = (DateTime)registrationDate
            };
            try
            {
                classRegistrationService.AddClassRegistration(classRegistration);
                MessageBox.Show("Create class registration successfully", "Notification!");
                ResetInput();
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoadClassCbo()
        {
            var listClass = classService.GetClasses();
            cboClass.Items.Clear();
            cboClass.ItemsSource = listClass;
            cboClass.DisplayMemberPath = "ClassId";
            cboClass.SelectedValuePath = "ClassId";
        }

        private void LoadMember()
        {
            var listMember = memberDetailService.GetMemberDetails();
            cboMember.Items.Clear();
            cboMember.ItemsSource = listMember;
            cboMember.DisplayMemberPath = "MemberId";
            cboMember.SelectedValuePath = "MemberId";
        }

        private void cboClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           var classId = cboClass.SelectedValue;
            var classDetail = classService.GetClassByClassId((int)classId);
            txtClassName.Text = classDetail.ClassName;
        }

        private void cboMember_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var memberId = cboMember.SelectedValue;
            var memberDetail = memberDetailService.GetMemberDetail((int)memberId);
            var user = userService.GetUserByUserId(memberDetail.UserId);
            txtMemberName.Text = user.FullName;
        }
        private void ResetInput()
        {
            txtClassName.Text = "";
            txtMemberName.Text = "";
            dpRegistrationDate.SelectedDate = null;
        }
    }
}
