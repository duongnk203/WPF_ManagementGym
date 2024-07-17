using BusinessObjects.Models;
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

namespace MainWindow.Member
{
    /// <summary>
    /// Interaction logic for CreateMemberDetail.xaml
    /// </summary>
    public partial class CreateMemberDetail : Window
    {
        private readonly IMembershipService membershipService;
        private readonly IUserService userService;
        private readonly IMemberDetailService memberDetailService;
        
        public CreateMemberDetail()
        {
            InitializeComponent();

            membershipService = new MembershipService();
            userService  = new UserService();
            memberDetailService = new MemberDetailService();

            LoadMemberShip();
        }

        private void btnCreateMember_Click(object sender, RoutedEventArgs e)
        {
            ValidationInput validation = new ValidationInput();
            var fullName = txtFullName.Text;
            if (!validation.InputName(fullName))
            {
                MessageBox.Show("Please enter full name", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            var dobString = dpDob.SelectedDate.ToString();
            if (!validation.InputDate(dobString)) return;
            DateOnly dateCondition = DateOnly.FromDateTime(dpDob.SelectedDate.Value);
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            if (currentDate.CompareTo(dateCondition) < 15)
            {
                MessageBox.Show("Member must have age than 15");
                return;
            }

            var email = txtEmail.Text;
            if (!validation.InputEmail(email)) return;

            var phone = txtPhone.Text;
            if (!validation.InputPhone(phone)) return;

            var address = txtAddress.Text;
            if (!validation.InputAddress(address)) return;

            var userName = txtUserName.Text;
            if (!validation.InputName(userName))
            {
                MessageBox.Show("Please enter username", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var listUser = userService.GetUserByRole("User");

            if (userName != null)
            {
                var userCheck = listUser.FirstOrDefault(u => u.Username.ToLower() == userName.ToLower());
                if (userCheck != null)
                {
                    MessageBox.Show("Username is exist in system", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }

            var password = txtPassword.Password;
            if (!validation.InputPassword(password)) return;

            if (!dpJoinDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Please enter join date", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var joinDate = dpJoinDate.SelectedDate.Value;

            if(cboMembership.SelectedValue == null)
            {
                MessageBox.Show("Please select membership", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var membershipId = cboMembership.SelectedValue.ToString();


            var user = new BusinessObjects.Models.User
            {
                FullName = fullName,
                Dob = DateOnly.FromDateTime(dpDob.SelectedDate.Value),
                Email = email,
                Phone = phone,
                Address = address,
                Username = userName,
                Password = password,
                Role = "User",
                Status = true
            };
            var userId = userService.GetUsers();

            var memberDetail = new MemberDetail()
            {
                UserId = userId.Last().UserId,
                MembershipId = int.Parse(membershipId),
                JoinDate = joinDate,
                Status = true
            };
            memberDetailService.AddMemberDetail(memberDetail);

            MessageBox.Show("Create member success!");
            ResetInput();


        }

        private void LoadMemberShip()
        {
            cboMembership.Items.Clear();
            var listMembership = membershipService.GetMemberships();
            cboMembership.ItemsSource = listMembership;
            cboMembership.DisplayMemberPath = "MembershipType";
            cboMembership.SelectedValuePath = "MembershipId";
        }

        private void ResetInput()
        {
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtFullName.Text = "";  
            txtPassword.Password = "";
            txtPhone.Text = "";
            txtUserName.Text = "";
            dpDob.Text = "";
            dpJoinDate.Text = "";
            cboMembership.SelectedIndex = 0;
            txtMemershipPrice.Text = "";

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cboMembership_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var membershipId = cboMembership.SelectedValue.ToString();
            var membership = membershipService.GetMembership(int.Parse(membershipId));
            txtMemershipPrice.Text = membership.Price.ToString();
        }
    }
}
