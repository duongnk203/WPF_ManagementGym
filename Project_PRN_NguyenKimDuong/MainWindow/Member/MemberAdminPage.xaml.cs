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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainWindow.Member
{
    /// <summary>
    /// Interaction logic for MemberAdminPage.xaml
    /// </summary>
    public partial class MemberAdminPage : Page
    {
        private readonly IMemberDetailService memberDetailService;
        private readonly IMembershipService membershipService;
        private readonly IUserService userService;
        private User userSelected;

        public MemberAdminPage()
        {
            InitializeComponent();

            memberDetailService = new MemberDetailService();
            membershipService = new MembershipService();
            userService = new UserService();

            LoadMembershipCbo();
            LoadListMemberDetails();
            LoadStauts();
        }

        private void LoadMembershipCbo()
        {
            cboMembership.Items.Clear();
            var listMembership = membershipService.GetMemberships();
            cboMembership.ItemsSource = listMembership;
            cboMembership.DisplayMemberPath = "MembershipType";
            cboMembership.SelectedValuePath = "MembershipId";
        }

        private void LoadListMemberDetails()
        {

            var memberDetails = memberDetailService.GetMemberDetails();
            dgListMembers.ItemsSource = memberDetails;
        }

        private void btnCreateMember_Click(object sender, RoutedEventArgs e)
        {
            CreateMemberDetail createMemberDetail = new CreateMemberDetail();
            createMemberDetail.ShowDialog();
        }

        private void btnUpdateMember_Click(object sender, RoutedEventArgs e)
        {
            var memberId = txtMemberId.Text.Trim();
            if (memberId == "")
            {
                MessageBox.Show("Please select member", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
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
                if(currentDate.CompareTo(dateCondition) < 15)
                {
                    MessageBox.Show("Member must have age than 15");
                    return;
                }

                var dob = DateTime.Parse(dobString);

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

                if (userName != null && !userName.ToLower().Equals(userSelected.Username.ToLower()))
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

                if (cboMembership.SelectedValue == null)
                {
                    MessageBox.Show("Please select membership", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var membershipId = cboMembership.SelectedValue.ToString();

                var status = cboStatus.SelectedValue;
                if (cboStatus.SelectedValue == null)
                {
                    MessageBox.Show("Please select status", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var user = new BusinessObjects.Models.User
                {
                    UserId = userSelected.UserId,
                    FullName = fullName,
                    Dob = DateOnly.FromDateTime(dob),
                    Email = email,
                    Phone = phone,
                    Address = address,
                    Username = userName,
                    Password = password,
                    Role = "User",
                    Status = status.ToString() == "Active" ? true : false
                };
                var userId = userService.GetUsers();

                var memberDetail = new MemberDetail()
                {
                    MemberId = int.Parse(txtMemberId.Text),
                    UserId = userId.Last().UserId,
                    MembershipId = int.Parse(membershipId),
                    JoinDate = joinDate,
                    Status = status.ToString() == "Active" ? true : false
                };

                userService.UpdateUser(user);
                memberDetailService.UpdateMemberDetail(memberDetail);
                LoadListMemberDetails();
                MessageBox.Show("Update member success", "Notification!");
            }
        }

        private void btnDeleteMember_Click(object sender, RoutedEventArgs e)
        {
            var memberId = txtMemberId.Text.Trim();
            if (memberId == "")
            {
                MessageBox.Show("Please select member", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                var result = MessageBox.Show("Do you want to delete this member?", "Notification!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    memberDetailService.DeleteMemberDetail(int.Parse(memberId));
                    LoadListMemberDetails();
                    MessageBox.Show("Delete member success", "Notification!");
                }
            }
        }

        private void dgListMembers_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                            if (int.TryParse(id, out int memberId))
                            {
                                MemberDetail memberDetail = memberDetailService.GetMemberDetail(memberId);
                                if (memberDetail != null)
                                {
                                    LoadMemberDetailInfo(memberDetail);
                                }
                            }
                        }
                    }
                }
            }
        }
        private void LoadMemberDetailInfo(MemberDetail memberDetail)
        {
            var user = userService.GetUserByUserId(memberDetail.UserId);
            userSelected = user;
            var membership = membershipService.GetMembership(memberDetail.MembershipId);

            txtFullName.Text = user.FullName;
            txtEmail.Text = user.Email;
            txtAddress.Text = user.Address;
            txtPassword.Password = user.Password;
            txtPhone.Text = user.Phone;
            txtUserName.Text = user.Username;
            dpDob.Text = user.Dob.ToString();
            txtMemberId.Text = memberDetail.MemberId.ToString();
            cboMembership.SelectedValue = memberDetail.MembershipId;
            txtPriceMembership.Text = membership.Price.ToString();
            cboStatus.SelectedValue = memberDetail.Status == true ? "Active" : "Inactive";
            dpJoinDate.Text = memberDetail.JoinDate.ToString();
        }

        private void LoadStauts()
        {
            cboStatus.Items.Clear();
            List<string> listStatus = ["Active", "Inactive"];
            cboStatus.ItemsSource = listStatus;
        }
    }
}
