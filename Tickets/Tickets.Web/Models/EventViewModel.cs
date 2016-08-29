using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tickets.Web.Models
{
    public class EventViewModel
    {
        public long EventId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public List<TicketViewModel> TicketViewModels { get; set; }
    }
}