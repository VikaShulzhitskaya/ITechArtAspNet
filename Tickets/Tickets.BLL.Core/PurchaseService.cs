using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.BLL.Common;
using Tickets.DAL.Interfaces;
using Tickets.DAL.Models.Entities;
using Tickets.DAL.Models.Enums;

namespace Tickets.BLL.Core
{
    public class PurchaseService : IPurchaseService
    {
        private readonly ITicketsUnitOfWork _uow;
        private readonly IRepository<Ticket> _ticketsRepository;
        private readonly IRepository<Purchase> _purchaseRepository;

        public PurchaseService(ITicketsUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
            _ticketsRepository = _uow.Tickets;
            _purchaseRepository = _uow.Purchases;
        }
        public IList<Purchase> GetPurchasesByUserId(string id)
        {
            return _purchaseRepository.GetAll().Include(p => p.User).Include(p => p.Ticket).Where(p => p.User.Id == id).ToList();
        }

        public IList<Purchase> GetUnconfirmedPurchasesByUserId(string id)
        {
            return
                _purchaseRepository.GetAll()
                    .Include(p => p.User)
                    .Include(p => p.Ticket)
                    .Where(p => p.UserId == id)
                    .Where(p => p.Status == PurchaseStatus.Unconfirmed).ToList();
        }

        public Purchase GetPurchase(long id)
        {
            return _purchaseRepository.GetById(id);
        }

        public bool ConfirmPurchase(long id)
        {
            var purchase = _purchaseRepository.GetById(id);
            var ticket = _ticketsRepository.GetById(purchase.TicketId);
            if (ticket.AvailableTicketsCount < purchase.NumberOfTickets)
            {
                return false;
            }
            try
            {
                ticket.AvailableTicketsCount -= purchase.NumberOfTickets;
                purchase.Status = PurchaseStatus.Confirmed;
                _ticketsRepository.Update(ticket);
                _purchaseRepository.Update(purchase);
                _uow.Complete();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CancelPurchase(long id)
        {
            var purchase = _purchaseRepository.GetById(id);
            var ticket = _ticketsRepository.GetById(purchase.TicketId);
            ticket.AvailableTicketsCount += purchase.NumberOfTickets;
            purchase.Status = PurchaseStatus.Canceled;
            try
            {
                _ticketsRepository.Update(ticket);
                _purchaseRepository.Update(purchase);
                _uow.Complete();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditPurchase(Purchase newPurchase)
        {
            var purchase = _purchaseRepository.GetById(newPurchase.Id);
            var ticket = _ticketsRepository.GetById(purchase.TicketId);
            if (ticket.AvailableTicketsCount < newPurchase.NumberOfTickets)
            {
                return false;
            }
            purchase.NumberOfTickets = newPurchase.NumberOfTickets;
            try
            {
                _purchaseRepository.Update(purchase);
                _uow.Complete();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddPurchase(Purchase purchase)
        {
            try
            {
                purchase.Status = PurchaseStatus.Unconfirmed;
                _purchaseRepository.Create(purchase);
                _uow.Complete();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
