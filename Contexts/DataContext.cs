using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ec_utbildningar_assignment_datastorage.Models.Entities.Entities;

namespace ec_utbildningar_assignment_datastorage.Contexts
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=SQL-DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }

        public void Seed()
        {
            if (!TicketStatuses.Any())
            {
                TicketStatuses.AddRange(new[]
                {
                new TicketStatus { Name = "Not Started" },
                new TicketStatus { Name = "In Progress" },
                new TicketStatus { Name = "Completed" }
            });
                SaveChanges();
            }
        }

    }

}


