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
using static Kanban.MainWindow;

namespace Kanban
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private List<MainWindow.User> users;

        public MainWindow.User LoggedUser { get; private set; }

        public Login()
        {

            InitializeComponent(); // sempre crida-ho primer per crear controls

            // Omplir la llista d'usuaris (hardcoded per ara)
            users = new List<MainWindow.User>()
            {
                new MainWindow.User { id = MainWindow.GenerarId(), nom = "usuari1", password = "1234", admin = true },
                new MainWindow.User { id = MainWindow.GenerarId(), nom = "usuari2", password = "1234", admin = false }
            };

            txtError.Visibility = Visibility.Collapsed;
        }
        private void IniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            string user = username.Text?.Trim();
            string pass = password.Text?.Trim();

            // Ejemplo simple: validación hardcoded (reemplaza por tu lógica real)
            var found = users.FirstOrDefault(u => u.nom == user && u.password == pass);

            if (found != null)
            {
                LoggedUser = found;
                this.DialogResult = true; // això fa que ShowDialog() retorni true
                // no cridar Close() explícitament si fas DialogResult = true
            }
            else
            {
                txtError.Text = "Usuari o contrasenya incorrectes";
                txtError.Visibility = Visibility.Visible;
            }
        }

    }
}
