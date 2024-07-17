using BusinessObjects.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MainWindow.Class
{
    /// <summary>
    /// Interaction logic for ClassAdminPage.xaml
    /// </summary>
    public partial class ClassAdminPage : Page
    {
        private readonly ITrainerService trainerService;
        private readonly IClassService classService;
        private readonly IScheduleService scheduleService;
        public ClassAdminPage()
        {
            InitializeComponent();

            trainerService = new TrainerService();
            classService = new ClassService();
            scheduleService = new ScheduleService();

            LoadListTrainer();
            LoadListClasses();
            LoadStatus();
            LoadlistSchedule();
        }

        private void LoadListTrainer()
        {
            cboTrainer.Items.Clear();
            var listTrainers = trainerService.GetTrainers();
            cboTrainer.ItemsSource = listTrainers;
            cboTrainer.DisplayMemberPath = "TrainerId";
            cboTrainer.SelectedValuePath = "TrainerId";
        }
        private void LoadListClasses()
        {
            var classes = classService.GetClasses();
            dgListClasses.ItemsSource = classes;
        }

        private void dgListClasses_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                            if (int.TryParse(id, out int classId))
                            {
                                BusinessObjects.Models.Class classSelected = classService.GetClassByClassId(classId);
                                if (classSelected != null)
                                {
                                    LoadClassInfo(classSelected);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void LoadClassInfo(BusinessObjects.Models.Class classSelected)
        {
            txtClassId.Text = classSelected.ClassId.ToString();
            txtClassName.Text = classSelected.ClassName;
            txtDescription.Text = classSelected.Description;
            cboTrainer.SelectedValue = classSelected.TrainerId;
            cboSchedule.SelectedValue = classSelected.ScheduleId;
            cboStatus.SelectedValue = classSelected.Status == true ? "Active" : "Inactive";
            txtNumber.Text = classSelected.Number.ToString();
        }

        private void LoadStatus()
        {
            cboStatus.Items.Clear();
            List<string> listStatus = ["Active", "Inactive"];
            cboStatus.ItemsSource = listStatus;
        }

        private void btnCreateClass_Click(object sender, RoutedEventArgs e)
        {
            CreateClass createClass = new CreateClass();
            createClass.ShowDialog();
            LoadListClasses();
        }

        private void LoadlistSchedule()
        {

            cboSchedule.Items.Clear();
            var listSchedule = scheduleService.GetSchedules();
            cboSchedule.ItemsSource = listSchedule;
            cboSchedule.DisplayMemberPath = "ScheduleName";
            cboSchedule.SelectedValuePath = "ScheduleId";
        }

        private void btnUpdateClass_Click(object sender, RoutedEventArgs e)
        {
            var classId = txtClassId.Text;
            if (classId == "")
            {
                MessageBox.Show("Please select a class to update", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
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
            var status = cboStatus.SelectedValue;
            if(status == null)
            {
                MessageBox.Show("Please select status", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var number = txtNumber.Text;
            if(!validation.InputNumber(number))
            {
                return;
            }

            BusinessObjects.Models.Class classUpdate = new BusinessObjects.Models.Class()
            {
                ClassId = int.Parse(classId),
                ClassName = className,
                Description = classDescription,
                TrainerId = int.Parse(trainerId.ToString()),
                ScheduleId = int.Parse(scheduleId.ToString()),
                Status = status.ToString() == "Active" ? true : false,
                Number = int.Parse(number)
            };

            try
            {
                classService.UpdateClass(classUpdate);
                MessageBox.Show("Update class successfully", "Notification!");
                LoadListClasses();
            }
            catch (DataAccessException ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeleteClass_Click(object sender, RoutedEventArgs e)
        {
            var classId = txtClassId.Text;
            if (classId == "")
            {
                MessageBox.Show("Please select a class to update", "Notification!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this class?", "Notification!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    classService.DeleteClass(int.Parse(classId));
                    MessageBox.Show("Delete class successfully", "Notification!");
                    LoadListClasses();
                }
                catch (DataAccessException ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
