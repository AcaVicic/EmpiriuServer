using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Class <c>DailyJournal</c> represents daily thoughts writen by <c>User</c>.
    /// </summary>
    public class DailyJournal
    {
        /// <summary>
        /// <value> 
        /// Property <c>Id</c> represents the daily journal's primary key.
        /// </value>
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// <value> 
        /// Property <c>Id</c> represents the daily journal's author.
        /// </value>
        /// </summary>
        public User? User { get; set; }
        /// <summary>
        /// <value> 
        /// Property <c>Id</c> represents the daily journal's text.
        /// </value>
        /// </summary>
        public string? Text { get; set; }
        /// <summary>
        /// <value> 
        /// Property <c>Id</c> represents the daily journal's publish date.
        /// </value>
        /// </summary>
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"{Id}, {User!.Id}, {Text}, {Date}";
        }
    }
}
