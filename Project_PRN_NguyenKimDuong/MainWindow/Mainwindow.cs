﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MainWindow.Trainer;
using MainWindow.Member;
using MainWindow.Class;
using MainWindow.Registration;
using MainWindow.Home;

namespace MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Mainwindow : Window
    {
        public Mainwindow()
        {
            InitializeComponent();
        }

        private void MyTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                TabItem selectedTab = (sender as TabControl).SelectedItem as TabItem;

                switch (selectedTab.Header.ToString())
                {
                    case "Home":
                        ContentFrame.Navigate(new HomePage());
                        break;
                    case "Trainers":
                        ContentFrame.Navigate(new TrainerAdminPage());
                        break;
                    case "Members":
                        ContentFrame.Navigate(new MemberAdminPage());
                        break;
                    case "Classes":
                        ContentFrame.Navigate(new ClassAdminPage());
                        break;
                    case "Registration":
                        ContentFrame.Navigate(new RegistrationPage());
                        break;
                }
            }
        }
    }
}