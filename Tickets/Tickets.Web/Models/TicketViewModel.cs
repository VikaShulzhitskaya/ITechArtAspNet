using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tickets.Web.Models
{
    public class TicketViewModel
    {
        public long TicketId { get; set; }
        public float Price { get; set; }
        public int AvailableCount { get; set; }
        public DateTime Date { get; set; }
    }
}