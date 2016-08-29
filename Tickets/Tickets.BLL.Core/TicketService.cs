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
        private readonly ITicketsUnitOfWork _uow;
        private readonly IRepository<Ticket> _repository;

        public TicketService(ITicketsUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _repository = _uow.Tickets;
        }
        public IList<Ticket> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Ticket GetTicket(long id)
        {
            return _repository.GetById(id);
        }

        public IList<Ticket> GetTicketsByEventId(int id)
        {
            return _repository.GetAll().Where(s => s.EventId == id).ToList();
        }

        public bool AddTicket(Ticket ticket)
        {
            try
            {
                _repository.Create(ticket);
                _uow.Complete();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
