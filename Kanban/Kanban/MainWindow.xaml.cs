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
        private User _loggedUser;

        public MainWindow()
        {
            InitializeComponent();
        }
        public MainWindow(User loggedUser):this()
        { 
            _loggedUser = loggedUser ?? throw new ArgumentNullException(nameof(loggedUser));

        
            List<User> users = new List<User>
            {
                _loggedUser,
                new User { id = GenerarId(), nom = "usuari2", password = "1234", admin = false }
            };

            List<task> tasques = new List<task>();

             Random rnd = new Random();
                string[] titols = { "Compra material", "Revisar codi", "Documentar mòdul", "Provar funcions",
                        "Implementar API", "Optimitzar consulta", "Corregir errors", "Preparar informe",
                        "Validar dades", "Configurar servidor" };

                string[] descripcions = { "Tasca automàtica generada.", "Acció pendent.",
                              "Cal revisar-ho.", "Generada per proves." };

                for (int i = 0; i < 10; i++)
                {
                    var idtask = GenerarId();
                    var estatActual = (task.estat)rnd.Next(0, 4);
                    var prioritatActual = (task.priority)rnd.Next(0, 3);
                    int any = rnd.Next(2020, 2030);    
                    int mes = rnd.Next(1, 13);          
                    int dia = rnd.Next(1, DateTime.DaysInMonth(any, mes) + 1); 

                    DateTime dateDeadline= new DateTime(any,mes, dia);
                    tasques.Add(new task()
                    {
                        id = idtask,
                        titul = titols[rnd.Next(titols.Length)],
                        Descripció = descripcions[rnd.Next(descripcions.Length)],
                        estatTasca = estatActual,
                        prioritat = prioritatActual,
                        deadline = dateDeadline,
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
                public string id { get; set; }
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
                
                public DateTime deadline { get; set; }
                public User usuari {  get; set; }
          

            }

        public class User
        {
            public string id { get; set; }
            public string nom { get; set; }
            public string password {  get; set; }
            public bool admin { get; set; }


        }




    }
}
