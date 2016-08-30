using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Tickets.BLL.Common;
using Tickets.DAL.Models.Entities;
using Tickets.DAL.Models.Enums;
using Tickets.Web.Models;

namespace Tickets.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IPurchaseService _purchaseService;
        private readonly ITicketService _ticketService;

        public CartController(IPurchaseService purchase, ITicketService ticket, IEventService eventService)
        {
            _purchaseService = purchase;
            _ticketService = ticket;
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
                purchasesModel.Add(new CartViewModel{
                    PurchaseId = purchase.Id,
                    EventName = _event.Name,
                    Price = ticket.Price,
                    Status = purchase.Status,
                    TicketCount = purchase.NumberOfTickets,
                    TicketDate = ticket.Date,
                    AvailableTicketCount = ticket.AvailableTicketsCount
                });
            }
            return View(purchasesModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeStatus(long purchaseId, int ticketCount, string action)
        {
            var purchase = _purchaseService.GetPurchase(purchaseId);
            if (action == "confirm")
            {
                purchase.NumberOfTickets = ticketCount;
                _purchaseService.EditPurchase(purchase);
                _purchaseService.ConfirmPurchase(purchase.Id);
            }
            else if(action == "cancel")
            {
                _purchaseService.CancelPurchase(purchaseId);
            }
            return RedirectToAction("Index", "Cart");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddPurchase(long ticketId, int ticketCount)
        {
            var userId = User.Identity.GetUserId();
            _purchaseService.AddPurchase(ticketId,userId,ticketCount);
            return RedirectToAction("Index", "Home");
        }
    }
}