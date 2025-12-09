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
    /// Lógica de interacción para Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private List<MainWindow.User> users;

        public Register(List<MainWindow.User> usersList)
        {
            InitializeComponent();
            users = usersList;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string user = username.Text.Trim();
            string pass = password.Password.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                txtError.Text = "L'usuari i/o contrasenya son obligatoris i no poden estar buits";
                txtError.Visibility = Visibility.Visible;
                return;
            }

            // Crear usuari
            var nouUsuari = new MainWindow.User
            {
                id = MainWindow.GenerarId(),
                nom = user,
                password = pass,
                admin = false
            };

            // Afegir-lo a la llista del Login
            users.Add(nouUsuari);

            // Tornar al login
            this.DialogResult = true;
        }
        private void BackTo_LogIn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
