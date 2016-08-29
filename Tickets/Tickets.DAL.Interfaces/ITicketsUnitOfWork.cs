using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.DAL.Models.Entities;

namespace Tickets.DAL.Interfaces
{
    public interface ITicketsUnitOfWork
    {
        void Complete();
        IRepository<User> Users { get; set; }
        IRepository<Event> Events { get; set; }
        IRepository<Ticket> Tickets { get; set; }
        IRepository<Purchase> Purchases { get; set; }

    }
}
