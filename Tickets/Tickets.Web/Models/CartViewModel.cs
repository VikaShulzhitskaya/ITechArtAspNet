using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tickets.DAL.Models.Entities;
using Tickets.DAL.Models.Enums;

namespace Tickets.Web.Models
{
    public class CartViewModel
    {
        public long PurchaseId { get; set; }
        public string EventName { get; set; }
        public DateTime TicketDate { get; set; }
        public float Price { get; set; }
        public int TicketCount { get; set; }
        public int AvailableTicketCount { get; set; }
        public PurchaseStatus Status { get; set; }

        public bool IsCompleted
        {
            get { return Status == PurchaseStatus.Confirmed; }
        }
    }
}