using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class EmpiriuContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Quote>? Quotes { get; set; }
        public DbSet<DailyJournal>? DailyJournals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Empiriu;");
        }
    }
}
