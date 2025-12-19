using Kanban.Controllers;
using Kanban.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Kanban
{
    public partial class userList : Window
    {
        private UserController userController;

        public userList()
        {
            InitializeComponent();
            userController = new UserController();
            Loaded += UserList_Loaded;
        }

        private async void UserList_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                var users = await userController.GetAllUsersAsync();
                var tasks = new List<User>();

                foreach (var u in users)
                {
                    int count = await userController.GetUserTaskCountAsync(u.Id);
                    u.NumTasques = count;
                    tasks.Add(u);
                }

                UsersGrid.ItemsSource = tasks;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando usuarios: {ex.Message}");
            }
        }

        private void BackToKanban_LogIn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var form = new UserForm { Owner = this };

            if (form.ShowDialog() == true)
            {
                var created = await userController.InsertUserAsync(form.User);

                if (created != null)
                {
                    MessageBox.Show($"Usuario {created.UserName} creado.");
                    await LoadUsersAsync();
                }
                else
                {
                    MessageBox.Show("No se pudo crear el usuario.");
                }
            }
        }

        private async void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem is User selectedUser)
            {
                var form = new UserForm(selectedUser) { Owner = this };

                if (form.ShowDialog() == true)
                {
                    bool updated = await userController.UpdateUserAsync(form.User.Id, form.User);

                    if (updated)
                    {
                        MessageBox.Show($"Usuario {form.User.UserName} actualizado.");
                        await LoadUsersAsync();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo actualizar el usuario.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona un usuario para modificar.");
            }
        }

        private async void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem is User selectedUser)
            {
                var result = MessageBox.Show(
                    $"¿Seguro que quieres eliminar al usuario {selectedUser.UserName}?",
                    "Confirmar eliminación",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning
                );

                if (result == MessageBoxResult.Yes)
                {
                    bool deleted = await userController.DeleteUserAsync(selectedUser.Id);

                    if (deleted)
                    {
                        MessageBox.Show("Usuario eliminado correctamente.");
                        await LoadUsersAsync();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo eliminar el usuario.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona un usuario para eliminar.");
            }
        }
    }
}
