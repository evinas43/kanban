using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Kanban
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            var login = new Login();
            bool? result = login.ShowDialog();

            if (result == true)
            {
                // Ahora usamos Kanban.Model.User directamente
                var main = new MainWindow(login.LoggedUser);
                main.Show();
            }
            else
            {
                Shutdown();
            }
        }
    }
}
