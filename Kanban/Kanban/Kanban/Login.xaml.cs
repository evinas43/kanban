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
    public partial class Login : Window
    {
        private readonly UserController userController;
        public User LoggedUser { get; private set; }

        public Login()
        {
            InitializeComponent();
            userController = new UserController();
            txtError.Visibility = Visibility.Collapsed;
        }

        private async void IniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            txtError.Visibility = Visibility.Collapsed;

            string usernameValue = username.Text?.Trim();
            string pass = password.Password?.Trim();

            //if (string.IsNullOrEmpty(usernameValue) || string.IsNullOrEmpty(passwordValue))
            //{
            //    txtError.Text = "Introdueix usuari i contrasenya";
            //    txtError.Visibility = Visibility.Visible;
            //    return;
            //}

            LoggedUser = await userController.LoginWithUsersAsync(usernameValue, pass);

            if (LoggedUser != null)
            {
                DialogResult = true;
            }
            else
            {
                txtError.Text = "Usuari o contrasenya incorrectes";
                txtError.Visibility = Visibility.Visible;
            }
        }

        private void Register_click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new Register();
            registerWindow.ShowDialog();
        }
    }
}
