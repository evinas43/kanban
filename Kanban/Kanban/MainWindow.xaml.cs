
using Kanban.Controllers;
using Kanban.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;



namespace Kanban
{
    public partial class MainWindow : Window
    {
        private User _loggedUser;
        private TaskController taskController;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(User loggedUser) : this()
        {
            _loggedUser = loggedUser ?? throw new ArgumentNullException(nameof(loggedUser));
            taskController = new TaskController();
            if (_loggedUser.IsAdmin == 1)
            {
                btnAdminPanel.Visibility = Visibility.Visible;
            }

            Loaded += MainWindow_Loaded;
        }
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadColumnsAsync();
        }

        private async Task LoadColumnsAsync()
        {
            try
            {
                toDoList.ItemsSource = await taskController.GetTasquesByEstatAsync(1);     
                doingList.ItemsSource = await taskController.GetTasquesByEstatAsync(2);    
                toReviewList.ItemsSource = await taskController.GetTasquesByEstatAsync(3); 
                doneList.ItemsSource = await taskController.GetTasquesByEstatAsync(4);    
            }
            catch (Exception)
            {
                MessageBox.Show("Error carregant les tasques del servidor");
            }
        }


        private async void TaskListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView lv && lv.SelectedItem is Tasques selectedTask)
            {
                bool admin = true;
                
                

            
                  var popup = new popUpxaml(selectedTask, true,admin);   
                popup.Owner = this;
                popup.ShowDialog();

                if (popup.DialogResult == true)
                {
                    await LoadColumnsAsync();
                }
            }
        }

        private async void AddTask_Click(object sender, RoutedEventArgs e)
        {
            var popup = new popUpxaml(true);
            popup.Owner = this;

            if (popup.ShowDialog() == true)
            {
                await LoadColumnsAsync();
            }
        }
        private void BtnAdminPanel_Click(object sender, RoutedEventArgs e)
        {
            var userPanel = new userList();
            userPanel.Owner = this;
            userPanel.ShowDialog();
        }

    }
}