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
    public class EventService:IEventService
    {
        private ITicketsUnitOfWork uow;
        private IRepository<Event> repository;

        public EventService(ITicketsUnitOfWork uow)
        {
            this.uow = uow;
            repository = this.uow.Events;
        }
        public IEnumerable<Event> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public Event GetEvent(long id)
        {
            return repository.GetById(id);
        }

        public bool AddEvent(Event newEvent)
        {
            try
            {
                repository.Create(newEvent);
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
