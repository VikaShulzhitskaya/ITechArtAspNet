using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Tickets.DAL.Interfaces;
using Tickets.DAL.Models.Entities;

namespace Tickets.DAL.Core
{
    public class TicketsUnitOfWork:ITicketsUnitOfWork
    {
        private DbContext _dbContext;

        public TicketsUnitOfWork(DbContext context)
        {
            _dbContext = context;
        }
        public void Complete()
        {
            _dbContext.SaveChanges();
        }
        [Inject]
        public IRepository<User> Users { get; set; }
        [Inject]
        public IRepository<Event> Events { get; set; }
        [Inject]
        public IRepository<Ticket> Tickets { get; set; }
        [Inject]
        public IRepository<Purchase> Purchases { get; set; }
    }
}
