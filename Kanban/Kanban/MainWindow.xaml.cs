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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kanban
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<User> users = new List<User>();
            var indentifier = GenerarId();
             users.Add(new User()
            {
                id = indentifier,
                nom = "usuari1",
                admin = true
            });
            users.Add(new User()
            {
                id = indentifier,
                nom = "usuari2",
                admin = false
            });

            List<task> tasques = new List<task>();

            Random rnd = new Random();
            string[] titols = { "Compra material", "Revisar codi", "Documentar mòdul", "Provar funcions",
                    "Implementar API", "Optimitzar consulta", "Corregir errors", "Preparar informe",
                    "Validar dades", "Configurar servidor" };

            string[] descripcions = { "Tasca automàtica generada.", "Acció pendent.",
                          "Cal revisar-ho.", "Generada per proves." };

            for (int i = 0; i < 10; i++)
            {
                var estatActual = (task.estat)rnd.Next(0, 4);
                var prioritatActual = (task.priority)rnd.Next(0, 3);
                tasques.Add(new task()
                {
                    titul = titols[rnd.Next(titols.Length)],
                    Descripció = descripcions[rnd.Next(descripcions.Length)],
                    estatTasca = estatActual,
                    prioritat = prioritatActual,
                    usuari = users[0]


                });
            }
             DataContext = tasques.Where(t => t.estatTasca == task.estat.pending).ToList();
                toDoList.ItemsSource = tasques.Where(t => t.estatTasca == task.estat.pending).ToList();
                doingList.ItemsSource = tasques.Where(t => t.estatTasca == task.estat.doing).ToList();
                toReviewList.ItemsSource = tasques.Where(t => t.estatTasca == task.estat.toReview).ToList();
                doneList.ItemsSource = tasques.Where(t => t.estatTasca == task.estat.Done).ToList();



        }
        public static string GenerarId()
                {
                    int longitud = 20;
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                    var random = new Random();
                    var sb = new StringBuilder();

                    for (int i = 0; i < longitud; i++)
                    {
                        sb.Append(chars[random.Next(chars.Length)]);
                    }

                    return sb.ToString();
                 }

        private void TaskListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView lv && lv.SelectedItem is Kanban.MainWindow.task selectedTask)
            {
                popUpxaml popup = new popUpxaml(selectedTask);
                popup.Owner = this;
                popup.ShowDialog();
            }
        }
        public class task
            {

                public string titul { get; set; }
                public string Descripció { get; set; }
                public enum estat
                {
                    pending = 0,
                    doing = 1,
                    toReview = 2,
                    Done = 3,

                }
                public enum priority 
                { 
                    low=0,
                    mid=1,
                    high=2,
                }

                public estat estatTasca { get; set; }
                public priority prioritat { get; set; }

                public User usuari {  get; set; }
          

            }

        public class User
        {
            public string id { get; set; }
            public string nom { get; set; }
            public bool admin { get; set; }


        }




    }
}
