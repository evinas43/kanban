using Kanban.Controllers;
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
using Kanban.Model;

namespace Kanban
{
    /// <summary>
    /// Lógica de interacción para Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private readonly UserController userController;

        public Register()
        {
            InitializeComponent();
            userController = new UserController();
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            string nameValue = name.Text.Trim();
            string surnameValue = surname.Text.Trim();
            string usernameValue = username.Text.Trim();
            string passwordValue = password.Password.Trim();

            if (string.IsNullOrEmpty(usernameValue) || string.IsNullOrEmpty(passwordValue))
            {
                txtError.Text = "L'usuari i/o contrasenya son obligatoris";
                txtError.Visibility = Visibility.Visible;
                return;
            }

            // Create user object
            Kanban.Model.User newUser = new Kanban.Model.User
            {
                UserName = usernameValue,
                Nom = nameValue,
                Cognom = surnameValue,
                Passwd = passwordValue,
                IsAdmin = 0
            };

            try
            {
                User createdUser = await userController.InsertUserAsync(newUser);

                if (createdUser != null)
                {
                    MessageBox.Show("Usuari creat correctament");
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    txtError.Text = "No s'ha pogut crear l'usuari";
                    txtError.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                txtError.Text = "Error de connexió amb el servidor";
                txtError.Visibility = Visibility.Visible;
            }
        }
    
        private void BackTo_LogIn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
