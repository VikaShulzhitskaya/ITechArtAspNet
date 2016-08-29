using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.DAL.Models.Entities;

namespace Tickets.BLL.Common
{
    public interface ITicketService
    {
        IList<Ticket> GetAll();
        Ticket GetTicket(long id);
        IList<Ticket> GetTicketsByEventId(int id);
        bool AddTicket(Ticket ticket);
    }
}
