using Kanban.Controllers;
using Kanban.Model;
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

namespace Kanban
{
    /// <summary>
    /// Lógica de interacción para userList.xaml
    /// </summary>
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
            await CargarUsuariosAsync();
        }

        private async Task CargarUsuariosAsync()
        {
            try
            {
                List<User> usuarios = await userController.GetAllUsersAsync();
                UsersGrid.ItemsSource = usuarios;
            }
            catch
            {
                MessageBox.Show("Error al cargar los usuarios desde la base de datos.");
            }
        }

        private void BackToKanban_LogIn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void AddUser_Click(object sender, RoutedEventArgs e)
        {
            // Aquí abrirías un formulario para crear un usuario
            MessageBox.Show("Formulario de creación de usuario.");
            await CargarUsuariosAsync(); // refrescar lista
        }

        private async void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem is User selectedUser)
            {
                // Abrir formulario para editar usuario
                MessageBox.Show($"Editar usuario: {selectedUser.UserName}");
                await CargarUsuariosAsync(); // refrescar lista tras editar
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
                if (MessageBox.Show($"¿Seguro que quieres eliminar al usuario {selectedUser.UserName}?",
                    "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    try
                    {
                        bool eliminado = await userController.DeleteUserAsync(selectedUser.Id);
                        if (eliminado)
                        {
                            MessageBox.Show("Usuario eliminado correctamente.");
                            await CargarUsuariosAsync(); // refrescar lista
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el usuario.");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Error al eliminar el usuario.");
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