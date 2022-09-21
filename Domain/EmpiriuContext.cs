using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    /// <summary>
    /// Class <c>EmpiriuContext</c> represents a session with Empiriu database.
    /// </summary>
    public class EmpiriuContext : DbContext
    {
        /// <summary>
        /// <value> 
        /// Property <c>Users</c> represents the collection od users from database.
        /// </value>
        /// </summary>
        public virtual DbSet<User>? Users { get; set; }
        /// <summary>
        /// <value> 
        /// Property <c>Users</c> represents the collection od daily journals from database.
        /// </value>
        /// </summary>
        public virtual DbSet<Quote>? Quotes { get; set; }
        /// <summary>
        /// <value> 
        /// Property <c>Users</c> represents the collection od quotes from database.
        /// </value>
        /// </summary>
        public virtual DbSet<DailyJournal>? DailyJournals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Empiriu;");
        }
    }
}
