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
    public class EventService : IEventService
    {
        private readonly ITicketsUnitOfWork _uow;
        private readonly IRepository<Event> _repository;

        public EventService(ITicketsUnitOfWork uow)
        {
            _uow = uow;
            _repository = _uow.Events;
        }
        public IList<Event> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public Event GetEvent(long id)
        {
            return _repository.GetById(id);
        }

        public bool AddEvent(Event newEvent)
        {
            try
            {
                _repository.Create(newEvent);
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
