using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tickets.BLL.Common;
using Tickets.DAL.Models.Entities;
using Tickets.Web.Models;

namespace Tickets.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventService _eventService;
        private readonly ITicketService _ticketService;
        private readonly IPurchaseService _purchaseService;

        public HomeController(IEventService eventService, ITicketService ticketService, IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
            _eventService = eventService;
            _ticketService = ticketService;
        }
        // GET: Home
        public ActionResult Index()
        {
            var eventsView = new List<EventViewModel>();
            var events = _eventService.GetAll();
            foreach (var _event in events)
            {
                var tickets = _ticketService.GetTicketsByEventId(_event.Id);
                var ticketsView = new List<TicketViewModel>();
                foreach (var _ticket in tickets)
                {
                    ticketsView.Add(new TicketViewModel {
                        TicketId = _ticket.Id,
                        AvailableCount = _ticket.AvailableTicketsCount,
                        Date = _ticket.Date,
                        Price = _ticket.Price
                    });
                }
                eventsView.Add(new EventViewModel
                {
                    EventId = _event.Id,
                    Description = _event.Description,
                    EventName = _event.Name,
                    TicketViewModels = ticketsView
                });
            }
            return View(eventsView);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult AddTicket()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult AddTicket(AddTicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                var _event = new Event {Name = model.EventName, Description = model.EventDescription};
                _eventService.AddEvent(_event);
                _ticketService.AddTicket(new Ticket
                {
                    EventId = _event.Id,
                    Date = model.Date,
                    AvailableTicketsCount = model.AvailableCount,
                    Price = model.Price
                });
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}