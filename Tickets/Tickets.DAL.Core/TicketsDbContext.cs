using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Tickets.DAL.Models.Entities;

namespace Tickets.DAL.Core
{
    public class TicketsDbContext:IdentityDbContext<User>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public TicketsDbContext() : base("CinemaTickets")
        {
            
        }

        public TicketsDbContext(string connectionString):base(connectionString)
        {
            
        }
    }
}
