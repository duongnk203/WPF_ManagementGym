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

namespace MainWindow.Trainer
{
    /// <summary>
    /// Interaction logic for CreateTraniner.xaml
    /// </summary>
    public partial class CreateTraniner : Window
    {
        private readonly ITrainerService trainerService;
        private readonly IUserService userService;
        public CreateTraniner()
        {
            InitializeComponent();

            trainerService = new TrainerService();
            userService = new UserService();
        }

        private void btnCreateTrainer_Click(object sender, RoutedEventArgs e)
        {
            ValidationInput validation = new ValidationInput();
            var fullName = txtFullName.Text;
            if (!validation.InputName(fullName))
            {
                MessageBox.Show("Please enter full name");
                return;
            }

            var dobString = dpDob.SelectedDate.ToString();
            if (!validation.InputDate(dobString)) return;

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
                MessageBox.Show("Please enter username");
                return;
            }

            var listUser = userService.GetUserByRole("Trainer");

            if (userName != null)
            {
                var userCheck = listUser.FirstOrDefault(u => u.Username.ToLower() == userName.ToLower());
                if (userCheck != null)
                {
                    MessageBox.Show("Username is exist in system");
                    return;
                }

            }

            var password = txtPassword.Password;
            if (!validation.InputPassword(password)) return;

            var specialization = txtSpecialization.Text;
            if (!validation.InputName(specialization))
            {
                MessageBox.Show("Please enter specialization");
                return;
            }
            var experience = txtExperience.Text;
            if (!validation.InputNumber(experience)) return;

            var user = new BusinessObjects.Models.User
            {
                FullName = fullName,
                Dob = DateOnly.FromDateTime(dob),
                Email = email,
                Phone = phone,
                Address = address,
                Username = userName,
                Password = password,
                Role = "Trainer",
                Status = true
            };
            var userId = userService.GetUsers();

            var trainer = new BusinessObjects.Models.Trainer
            {
                ExperienceYears = int.Parse(experience),
                Specialization = specialization,
                UserId = userId.Last().UserId,
                Status = true
                
            };

            userService.AddUser(user);
            trainerService.AddTrainer(trainer);
            MessageBox.Show("Create trainer success");
            ResetInput();
        }

        private void ResetInput()
        {
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtExperience.Text = "";
            txtFullName.Text = "";
            txtPassword.Password = "";
            txtSpecialization.Text = "";
            txtUserName.Text = "";
            txtPhone.Text = "";
            dpDob.SelectedDate = null;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
