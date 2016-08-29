using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Tickets.BLL.Common;
using Tickets.DAL.Models.Entities;
using Tickets.Web.Models;

namespace Tickets.Web.Controllers
{
    public class CartController : Controller
    {
        private IPurchaseService purchaseService;
        private ITicketService ticketService;
        private IEventService eventService;

        public CartController(IPurchaseService purchase, ITicketService ticket, IEventService eventService)
        {
            purchaseService = purchase;
            ticketService = ticket;
            this.eventService = eventService;
        }
        // GET: Cart
        [Authorize]
        public ActionResult Index()
        {
            var purchasesModel = new List<CartModel>();
            var userPurchases = purchaseService.GetPurchasesByUserId(User.Identity.GetUserId());
            foreach (Purchase purchase in userPurchases)
            {
                var ticket = ticketService.GetTicket(purchase.TicketId);
                var _event = ticket.Event;
                purchasesModel.Add(new CartModel {PurchaseId = purchase.Id,EventName = _event.Name,Price = ticket.Price,Status = purchase.Status,TicketCount = purchase.NumberOfTickets,TicketDate = ticket.Date});
            }
            return View();
        }
    }
}