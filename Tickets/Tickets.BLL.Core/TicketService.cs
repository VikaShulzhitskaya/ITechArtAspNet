using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.BLL.Common;
using Tickets.DAL.Interfaces;
using Tickets.DAL.Models.Entities;

namespace Tickets.BLL.Core
{
    public class TicketService:ITicketService
    {
        private ITicketsUnitOfWork uow;
        private IRepository<Ticket> repository;

        public TicketService(ITicketsUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
            repository = uow.Tickets;
        }
        public IEnumerable<Ticket> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public Ticket GetTicket(long id)
        {
            return repository.GetById(id);
        }

        public IEnumerable<Ticket> GetTicketsByEventId(int id)
        {
            return repository.GetAll().Where(s => s.EventId == id).ToList();
        }

        public bool AddTicket(Ticket ticket)
        {
            try
            {
                repository.Create(ticket);
                uow.Complete();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
