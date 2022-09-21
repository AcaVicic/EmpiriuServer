using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DailyJournal
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"{Id}, {User.Id}, {Text}, {Date}";
        }
    }
}
