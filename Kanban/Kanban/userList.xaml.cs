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
        }

        private void BackToKanban_LogIn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
