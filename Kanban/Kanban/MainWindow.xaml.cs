using Kanban.Controllers;
using Kanban.Model;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kanban
{
    public partial class MainWindow : Window
    {
        private User _loggedUser;
        private TaskController taskController;
        private UserController userController;
        private int permision;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(User loggedUser) : this()
        {
            _loggedUser = loggedUser ?? throw new ArgumentNullException(nameof(loggedUser));
            taskController = new TaskController();
            userController = new UserController();

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Load permissions after the window is loaded
            await LoadUserPermissionAsync();

            // Load the tasks after permission is loaded
            await LoadColumnsAsync();
        }

        private async Task LoadUserPermissionAsync()
        {
            try
            {
                // Call the API to get the user by ID
                User userFromApi = await userController.GetUserByIdAsync(_loggedUser.Id);

                if (userFromApi != null && userFromApi.IsAdmin == 1)
                {
                    permision = 1; // User has admin rights
                    btnAdminPanel.Visibility = Visibility.Visible; // Show admin panel
                }
                else
                {
                    permision = 0; // User does not have admin rights
                    btnAdminPanel.Visibility = Visibility.Collapsed; // Hide admin panel
                }
            }
            catch (Exception ex)
            {
                // Handle errors and set the visibility of the admin panel to hidden if there's an error
                MessageBox.Show("Error loading user permissions");
                btnAdminPanel.Visibility = Visibility.Collapsed;
            }
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
                bool admin = false;
                Console.WriteLine(_loggedUser.Id);

                // If the user is an admin, set the admin flag to true
                if (_loggedUser.IsAdmin == 1)
                {
                    admin = true;
                }

                var popup = new popUpxaml(selectedTask, true, admin);
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
