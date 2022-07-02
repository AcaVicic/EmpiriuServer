using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Quote
    {
        public int Id { get; set; }
        public string? Philosopher { get; set; }    
        public string? Text { get; set; }
        public string? Image { get; set; }
        public string? AboutPhilosopher { get; set; }
        public string? Explanation { get; set; }
        public string? Book { get; set; }
    }
}
