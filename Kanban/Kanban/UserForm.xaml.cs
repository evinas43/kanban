using Kanban.Model;
using System.Windows;
using System.Windows.Media;

namespace Kanban
{
    public partial class UserForm : Window
    {
        public User User { get; private set; }
        private bool isEditMode;

        public UserForm(User user = null)
        {
            InitializeComponent();

            if (user != null)
            {
                // Edit mode
                isEditMode = true;
                this.Title = "⚙️ Editar Usuario";

                User = new User
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Nom = user.Nom,
                    Cognom = user.Cognom,
                    Passwd = user.Passwd,
                    IsAdmin = user.IsAdmin
                };

                UsernameBox.Text = User.UserName;
                NomBox.Text = User.Nom;
                CognomBox.Text = User.Cognom;
                PasswdBox.Password = User.Passwd;
                IsAdminCheck.IsChecked = User.IsAdmin == 1;

                ActionButton.Content = "💾 Guardar";
                ActionButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2962FF")); // Blue
                ActionButton.Foreground = Brushes.White;
            }
            else
            {
                // Create mode
                isEditMode = false;
                this.Title = "➕ Crear Usuario";

                User = new User();

                ActionButton.Content = "➕ Crear";
                ActionButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00C853")); // Green
                ActionButton.Foreground = Brushes.White;
            }
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            User.UserName = UsernameBox.Text;
            User.Nom = NomBox.Text;
            User.Cognom = CognomBox.Text;
            User.Passwd = PasswdBox.Password;
            User.IsAdmin = IsAdminCheck.IsChecked == true ? 1 : 0;

            if (string.IsNullOrWhiteSpace(User.UserName) || string.IsNullOrWhiteSpace(User.Passwd))
            {
                MessageBox.Show("Username y password son obligatorios.");
                return;
            }

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
