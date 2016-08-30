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

        public HomeController(IEventService eventService, ITicketService ticketService)
        {
            _eventService = eventService;
            _ticketService = ticketService;
        }
        // GET: Home
        public ActionResult Index(int page = 1, int pageSize = 20)
        {
            var eventsView = new List<EventViewModel>();
            var totalEvents = _eventService.GetAll();
            var events = _eventService.GetAll().Skip((page - 1)*pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalEvents.Count()
            };
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
            IndexEventViewModel indexEvent = new IndexEventViewModel
            {
                PageInfo = pageInfo,
                EventViewModels = eventsView
            };
            return View(indexEvent);
            //return View(eventsView);
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