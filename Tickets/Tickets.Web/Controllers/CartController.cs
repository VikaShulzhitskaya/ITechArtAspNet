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
        private readonly IPurchaseService _purchaseService;
        private readonly ITicketService _ticketService;
        private readonly IEventService _eventService;

        public CartController(IPurchaseService purchase, ITicketService ticket, IEventService eventService)
        {
            _purchaseService = purchase;
            _ticketService = ticket;
            _eventService = eventService;
        }
        // GET: Cart
        [Authorize]
        public ActionResult Index()
        {
            var purchasesModel = new List<CartViewModel>();
            var userPurchases = _purchaseService.GetPurchasesByUserId(User.Identity.GetUserId());
            foreach (Purchase purchase in userPurchases)
            {
                var ticket = _ticketService.GetTicket(purchase.TicketId);
                var _event = ticket.Event;
                purchasesModel.Add(new CartViewModel {PurchaseId = purchase.Id,EventName = _event.Name,Price = ticket.Price,Status = purchase.Status,TicketCount = purchase.NumberOfTickets,TicketDate = ticket.Date});
            }
            return View();
        }
    }
}