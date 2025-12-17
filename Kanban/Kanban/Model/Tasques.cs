using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Model
{
    public class Tasques
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Descripcio { get; set; }
        public int? UsuariId { get; set; }
        public int? Prioritat { get; set; }
        public int? Estat { get; set; }
        public DateTime? DataFinal { get; set; }
    }
}
