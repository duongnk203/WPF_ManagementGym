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

namespace MainWindow.Class
{
    /// <summary>
    /// Interaction logic for CreateClass.xaml
    /// </summary>
    public partial class CreateClass : Window
    {
        private readonly ITrainerService trainerService;
        private readonly IClassService classService;
        private readonly IScheduleService scheduleService;
        private readonly IUserService userService;

        public CreateClass()
        {
            InitializeComponent();

            trainerService = new TrainerService();
            classService = new ClassService();
            scheduleService = new ScheduleService();
            userService = new UserService();

            LoadSchedule();
            LoadTrainer();
        }

        private void LoadSchedule()
        {
            var list = scheduleService.GetSchedules();
            cboSchedule.ItemsSource = list;
            cboSchedule.DisplayMemberPath = "ScheduleName";
            cboSchedule.SelectedValuePath = "ScheduleId";
        }

        private void LoadTrainer()
        {
            var list = trainerService.GetTrainers();
            cboTrainer.ItemsSource = list;
            cboTrainer.DisplayMemberPath = "TrainerId";
            cboTrainer.SelectedValuePath = "TrainerId";
        }

        private void cboTrainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var trainerId = cboTrainer.SelectedValue.ToString();
            var trainer = trainerService.GetTrainerById(int.Parse(trainerId));
            var userId = trainer.UserId;
            var user = userService.GetUserByUserId(userId);
            txtTrainerName.Text = user.FullName;
        }

        private void btnCreateClass_Click(object sender, RoutedEventArgs e)
        {
            ValidationInput validation = new ValidationInput();
            var className = txtClassName.Text;
            if (!validation.InputName(className))
            {
                MessageBox.Show("Please enter class name", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var classDescription = txtDescription.Text;
            if (!validation.InputName(classDescription))
            {
                MessageBox.Show("Please enter class description", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var scheduleId = cboSchedule.SelectedValue;
            if (scheduleId == null)
            {
                MessageBox.Show("Please select schedule", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var trainerId = cboTrainer.SelectedValue;
            if (trainerId == null)
            {
                MessageBox.Show("Please select trainer", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var number = txtNumber.Text;
            if (!validation.InputNumber(number))
            {
                return;
            }

            BusinessObjects.Models.Class newClass = new BusinessObjects.Models.Class()
            {
                ClassName = className,
                Description = classDescription,
                TrainerId = int.Parse(trainerId.ToString()),
                ScheduleId = int.Parse(scheduleId.ToString()),
                Status = true,
                Number = int.Parse(number)
            };
            try
            {
                classService.AddClass(newClass);
                MessageBox.Show("Create class successfully", "Notification!");
                ResetInput();
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ResetInput()
        {
            txtClassName.Text = "";
            txtDescription.Text = "";
            cboSchedule.SelectedValue = null;
            txtTrainerName.Text = "";
            txtNumber.Text = "";
        }
    }

}
