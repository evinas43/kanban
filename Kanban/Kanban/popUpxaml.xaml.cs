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
    /// Lógica de interacción para popUpxaml.xaml
    /// </summary>
    public partial class popUpxaml : Window
    {
        public bool _esNueva;
        public MainWindow.task Tasca { get; set; }
        public Array estats => Enum.GetValues(typeof(MainWindow.task.estat));
        public Array prioritats => Enum.GetValues(typeof(MainWindow.task.priority));


        // Modo visualización
        public popUpxaml(MainWindow.task t)
        {
            InitializeComponent();  
            _esNueva = false;
            Tasca = t;

            DataContext = Tasca;   
            ActivarModoLectura();
        }

        // Modo creación
        public popUpxaml(bool esNueva)
        {
            InitializeComponent();
            _esNueva = esNueva;

            Tasca = new MainWindow.task
            {
                id = MainWindow.GenerarId(),
                deadline = DateTime.Now
            };

            DataContext = Tasca;  
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

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true; // para que MainWindow sepa que se guardó
            Close();
        }
    }

}
