using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kanban.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Nom { get; set; }
        public string Cognom { get; set; }

        public int IsAdmin { get; set; }
        public string Passwd { get; set; }
    }
}
