using BusinessObjects.Models;
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
        public ClassAdminPage()
        {
            InitializeComponent();

            trainerService = new TrainerService();
            classService = new ClassService();

            LoadListTrainer();
            LoadListClasses();
            LoadStatus();
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
           txtClassName.Text = classSelected.ClassName;
            txtDescription.Text = classSelected.Description;
            cboTrainer.SelectedValue = classSelected.TrainerId;
            dpSchedule.Text = classSelected.Schedule.ToString();
            cboStatus.SelectedValue = classSelected.Status == true ? "Active" : "Inactive";
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
        }
    }
}
