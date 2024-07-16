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
using Models = BusinessObjects.Models;

namespace MainWindow.Trainer
{
    /// <summary>
    /// Interaction logic for TrainerAdminPage.xaml
    /// </summary>
    public partial class TrainerAdminPage : Page
    {
        private readonly ITrainerService trainerService;
        private readonly IUserService userService;
        private readonly IClassService classService;
        private Models.User userSelected;

        public TrainerAdminPage()
        {
            InitializeComponent();

            trainerService = new TrainerService();
            userService = new UserService();
            classService = new ClassService();

            LoadList();
            LoadStatus();
        }

        private void LoadList()
        {
            var trainers = trainerService.GetTrainers();
            dgListTrainers.ItemsSource = trainers;
        }

        private void dgListTrainers_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                            if (int.TryParse(id, out int trainerId))
                            {
                                BusinessObjects.Models.Trainer trainer = trainerService.GetTrainerById(trainerId);
                                if (trainer != null)
                                {
                                    LoadTrainerInfo(trainer);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void LoadTrainerInfo(BusinessObjects.Models.Trainer trainer)
        {
            txtTrainerId.Text = trainer.TrainerId.ToString();
            txtExperience.Text = trainer.ExperienceYears.ToString();
            txtSpecialization.Text = trainer.Specialization;
            var user = userService.GetUserByUserId(trainer.UserId);
            txtEmail.Text = user.Email;
            txtFullName.Text = user.FullName;
            txtPhone.Text = user.Phone;
            txtUserName.Text = user.Username;
            txtPassword.Password = user.Password;
            txtAddress.Text = user.Address;
            dpDob.Text = user.Dob.ToString();
            cboStatus.SelectedValue = trainer.Status == true ? "Active" : "Inactive";

            userSelected = user;
            var countClass = classService.GetClassesByTrainerId(trainer.TrainerId).Count;
            txtNumberClasses.Text = countClass.ToString();
        }

        private void btnCreateTrainer_Click(object sender, RoutedEventArgs e)
        {
            CreateTraniner createTraniner = new CreateTraniner();
            createTraniner.ShowDialog();
            LoadList();
        }

        private void btnUpdateTrainer_Click(object sender, RoutedEventArgs e)
        {
            var trainerId = int.Parse(txtTrainerId.Text);
            if (txtTrainerId.Text.Trim() == "")
            {
                MessageBox.Show("Please select a trainer to update!", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
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

                var dob = DateTime.Parse(dobString);

                var email = txtEmail.Text;
                if (!validation.InputEmail(email)) return;
                if (!email.ToLower().Equals(userSelected.Email.ToLower()))
                {
                    var isExistEmail = userService.IsExistEmail(email);
                    MessageBox.Show("Email is exist in the system");
                    return;
                }

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

                var listUser = userService.GetUserByRole("Trainer");

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

                var specialization = txtSpecialization.Text;
                if (!validation.InputName(specialization))
                {
                    MessageBox.Show("Please enter specialization", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var experience = txtExperience.Text;
                if (!validation.InputNumber(experience)) return;

                var status = cboStatus.SelectedValue;
                if(cboStatus.SelectedValue == null)
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
                    Role = "Trainer",
                    Status = status.ToString() == "Active" ? true : false
                };
                var userId = userService.GetUsers();

                var trainer = new BusinessObjects.Models.Trainer
                {
                    TrainerId = trainerId,
                    ExperienceYears = int.Parse(experience),
                    Specialization = specialization,
                    UserId = userSelected.UserId,
                    Status = status.ToString() == "Active" ? true : false
                };

                trainerService.UpdateTrainer(trainer);
                userService.UpdateUser(user);
                MessageBox.Show("Update trainer success!", "Notification");
                LoadList();
                LoadTrainerInfo(trainer);
            }
        }

        private void btnDeleteTrainer_Click(object sender, RoutedEventArgs e)
        {
            var trainerId = int.Parse(txtTrainerId.Text);
            if (txtTrainerId.Text.Trim() == "")
            {
                MessageBox.Show("Please select a trainer to update!", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                var trainer = trainerService.GetTrainerById(trainerId);
                if (trainer != null)
                {
                    trainer.Status = false;
                    var result = MessageBox.Show("Are you sure you want to delete this trainer?", "Notification", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        trainerService.UpdateTrainer(trainer);
                        MessageBox.Show("Delete trainer success!", "Notification");
                        LoadList();
                        ResetInput();
                    }
                }
            }
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

        private void LoadStatus()
        {
            var listStatus = new List<string> { "Active", "Inactive" };
            cboStatus.ItemsSource = listStatus;
        }
    }


}
