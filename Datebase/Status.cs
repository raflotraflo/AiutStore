using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datebase
{
    public class Status
    {
        public int Id { get; set; }
        public string Info { get; set; }
        public bool State { get; set; }

        public Status()
        {
            Id = 0;
            Info = "Error";
            State = false;
        }
    }
}
