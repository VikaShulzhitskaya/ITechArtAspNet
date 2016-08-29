﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tickets.DAL.Models.Entities;
using Tickets.DAL.Models.Enums;

namespace Tickets.Web.Models
{
    public class CartModel
    {
        public long PurchaseId { get; set; }
        public string EventName { get; set; }
        public DateTime TicketDate { get; set; }
        public float Price { get; set; }
        public int TicketCount { get; set; }
        public PurchaseStatus Status { get; set; }
    }
}