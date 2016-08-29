using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.DAL.Models.Entities;

namespace Tickets.BLL.Common
{
    public interface IEventService
    {
        IEnumerable<Event> GetAll();
        Event GetEvent(long id);
        bool AddEvent(Event newEvent);
    }
}
