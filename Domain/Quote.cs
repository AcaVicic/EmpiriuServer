using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Class <c>Quote</c> represents daily philosophers quote.
    /// </summary>
    public class Quote
    {
        /// <summary>
        /// <value> 
        /// Property <c>Id</c> represents the quote's primary key.
        /// </value>
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// <value> 
        /// Property <c>Philosopher</c> represents the quote's author.
        /// </value>
        /// </summary>
        public string? Philosopher { get; set; }
        /// <summary>
        /// <value> 
        /// Property <c>Id</c> represents the quote itself.
        /// </value>
        /// </summary>
        public string? Text { get; set; }
        /// <summary>
        /// <value> 
        /// Property <c>Id</c> represents the quote's author's image url.
        /// </value>
        /// </summary>
        public string? Image { get; set; }
        /// <summary>
        /// <value> 
        /// Property <c>Id</c> represents the quote's author's biography.
        /// </value>
        /// </summary>
        public string? AboutPhilosopher { get; set; }
        /// <summary>
        /// <value> 
        /// Property <c>Id</c> represents the quote's explanation.
        /// </value>
        /// </summary>
        public string? Explanation { get; set; }
        /// <summary>
        /// <value> 
        /// Property <c>Id</c> represents the quote's book of origin.
        /// </value>
        /// </summary>
        public string? Book { get; set; }
    }
}
