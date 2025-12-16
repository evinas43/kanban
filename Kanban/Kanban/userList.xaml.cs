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
      
            public userList()
            {
                InitializeComponent();

                // Crear lista de usuarios de ejemplo
                var users = new List<User>
            {
                new User { Username = "jordi123", Nom = "Jordi", Cognom = "Pérez", Admin = true, NumTasques = 5 },
                new User { Username = "anna88", Nom = "Anna", Cognom = "Martí", Admin = false, NumTasques = 3 },
                new User { Username = "pau77", Nom = "Pau", Cognom = "Gómez", Admin = false, NumTasques = 7 }
            };

                // Asignar lista al DataGrid
                UsersGrid.ItemsSource = users;
            }

            private void BackToKanban_LogIn(object sender, RoutedEventArgs e)
            {
                this.Close();
            }

            private void AddUser_Click(object sender, RoutedEventArgs e)
            {
                MessageBox.Show("Aquí se abriría el formulario para añadir un usuario.");
            }

            private void EditUser_Click(object sender, RoutedEventArgs e)
            {
                if (UsersGrid.SelectedItem is User selectedUser)
                {
                    MessageBox.Show($"Aquí se editaría el usuario: {selectedUser.Username}");
                }
                else
                {
                    MessageBox.Show("Selecciona un usuario para modificar.");
                }
            }

            private void DeleteUser_Click(object sender, RoutedEventArgs e)
            {
                if (UsersGrid.SelectedItem is User selectedUser)
                {
                    MessageBox.Show($"Aquí se eliminaría el usuario: {selectedUser.Username}");
                }
                else
                {
                    MessageBox.Show("Selecciona un usuario para eliminar.");
                }
            }
        

        // Clase de ejemplo para los usuarios
        public class User
        {
            public string Username { get; set; }
            public string Nom { get; set; }
            public string Cognom { get; set; }
            public bool Admin { get; set; }
            public int NumTasques { get; set; }
        }
    }
}