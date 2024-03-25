using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MementoMori
    {

        public int Id { get; set; }

        public int FilledNumber { get; set; }

        public User? User { get; set; }

        public DateTime LastDate { get; set; }

    }
}
