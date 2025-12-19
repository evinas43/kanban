using Kanban.Controllers;
using Kanban.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Kanban
{


    public partial class popUpxaml : Window
    {
        public bool _esNueva;
        private bool _isAdmin;

        public Tasques Tasca { get; set; }

        private TaskController taskController;
        private UserController userController;


        public popUpxaml(Tasques tasca, bool soloEditarEstado, bool isAdmin)
        {
            InitializeComponent();

            taskController = new TaskController();
            userController = new UserController();
            Tasca = tasca;
            DataContext = Tasca;

            _isAdmin = isAdmin;

            _esNueva = false;

            CargarCombos();

            if (soloEditarEstado)
            {

                ActivarModoEditarEstado();
            }
            if (_isAdmin)
                btnBorrar.Visibility = Visibility.Visible;
            else
                ActivarModoLectura();
        }

        // Modo creación
        public popUpxaml(bool isNew)
        {
            InitializeComponent();

            taskController = new TaskController();
            userController = new UserController();

            Tasca = new Tasques();
            DataContext = Tasca;

            _esNueva = true;

            CargarCombos();
            ActivarModoEdicion();
        }

        private void ActivarModoLectura()
        {
            txtTitulo.IsReadOnly = true;
            txtDescripcion.IsReadOnly = true;
            comboEstat.IsEnabled = false;
            comboPrioridad.IsEnabled = false;
            btnGuardar.Visibility = Visibility.Collapsed;
        }

        private void ActivarModoEdicion()
        {
            txtTitulo.IsReadOnly = false;
            txtDescripcion.IsReadOnly = false;
            comboEstat.IsEnabled = true;
            comboPrioridad.IsEnabled = true;
            btnGuardar.Visibility = Visibility.Visible;
        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void Guardar_Click(object sender, RoutedEventArgs e)
        {
            Tasca.Nom = txtTitulo.Text;
            Tasca.Descripcio = txtDescripcion.Text;
            Tasca.Prioritat = comboPrioridad.SelectedIndex;
            Tasca.Estat = comboEstat.SelectedIndex + 1;
            Tasca.DataFinal = FinalDate.SelectedDate;
            Tasca.UsuariId = comboUsuario.SelectedValue as int?;


            if (_esNueva || comboUsuario.IsEnabled)
            {
                if (comboUsuario.SelectedValue == null)
                {
                    MessageBox.Show("Debes seleccionar un usuario");
                    return;
                }
                Tasca.UsuariId = (int)comboUsuario.SelectedValue;
            }
            Tasca.UsuariId = (int)comboUsuario.SelectedValue;

            try
            {
                if (_esNueva)
                    await taskController.InsertTascaAsync(Tasca);
                else
                    await taskController.UpdateTascaAsync(Tasca.Id, Tasca);

                DialogResult = true;
                Close();
            }
            catch
            {
                MessageBox.Show("Error guardant la tasca");
            }
        }
        private void CargarCombos()
        {
            comboEstat.ItemsSource = new[]
            {"To Do", "Doing", "To Review", "Done"};

            comboPrioridad.ItemsSource = new[] { "Low", "Mid", "High" };

            if (Tasca.Estat.HasValue)
                comboEstat.SelectedIndex = Tasca.Estat.Value - 1;

            if (Tasca.Prioritat.HasValue)
                comboPrioridad.SelectedIndex = Tasca.Prioritat.Value;

            CargarUsuarios();
        }
        private async void CargarUsuarios()
        {
            try
            {
                var users = await userController.GetAllUsersAsync(); // Necesitas este método en tu controller
                comboUsuario.ItemsSource = users;

                if (Tasca.UsuariId.HasValue)
                    comboUsuario.SelectedValue = Tasca.UsuariId.Value;
            }
            catch
            {
                MessageBox.Show("Error cargando usuarios");
            }
        }
        private void ActivarModoEditarEstado()
        {
            // Bloqueamos todo excepto el estado
            txtTitulo.IsReadOnly = true;
            txtDescripcion.IsReadOnly = true;
            FinalDate.IsEnabled = false;
            comboUsuario.IsEnabled = false;
            comboPrioridad.IsEnabled = false;

            comboEstat.IsEnabled = true; // Solo editable
            btnGuardar.Visibility = Visibility.Visible; // Mostramos botón guardar
        }
        private async void Borrar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("¿Seguro que quieres borrar esta tarea?", "Confirmar", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    bool deleted = await taskController.DeleteTascaAsync(Tasca.Id);
                    if (deleted)
                    {
                        DialogResult = true; // Para que se refresque la lista
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo borrar la tarea");
                    }
                }
                catch
                {
                    MessageBox.Show("Error al borrar la tarea");
                }
            }
        }
    }
}
