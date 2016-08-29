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
    public class PurchaseService:IPurchaseService
    {
        private ITicketsUnitOfWork uow;
        private IRepository<Ticket> ticketsRepository;
        private IRepository<User> userRepository;
        private IRepository<Purchase> purchaseRepository;

        public PurchaseService(ITicketsUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
            ticketsRepository = uow.Tickets;
            userRepository = uow.Users;
            purchaseRepository = uow.Purchases;
        }
        public IEnumerable<Purchase> GetPurchasesByUserId(string id)
        {
            return purchaseRepository.GetAll().Include(p => p.User).Include(p => p.Ticket).Where(p => p.User.Id == id).ToList();
        }

        public IEnumerable<Purchase> GetUnconfirmedPurchasesByUserId(string id)
        {
            return
                purchaseRepository.GetAll()
                    .Include(p => p.User)
                    .Include(p => p.Ticket)
                    .Where(p => p.UserId == id)
                    .Where(p => p.Status == PurchaseStatus.Unconfirmed).ToList();
        }

        public Purchase GetPurchase(long id)
        {
            return purchaseRepository.GetById(id);
        }

        public bool ConfirmPurchase(long id)
        {
            var purchase = purchaseRepository.GetById(id);
            var ticket = ticketsRepository.GetById(purchase.TicketId);
            if (ticket.AvailableTicketsCount < purchase.NumberOfTickets)
                return false;
            try
            {
                ticket.AvailableTicketsCount -= purchase.NumberOfTickets;
                purchase.Status = PurchaseStatus.Confirmed;
                ticketsRepository.Update(ticket);
                purchaseRepository.Update(purchase);
                uow.Complete();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CancelPurchase(long id)
        {
            var purchase = purchaseRepository.GetById(id);
            var ticket = ticketsRepository.GetById(purchase.TicketId);
            ticket.AvailableTicketsCount += purchase.NumberOfTickets;
            purchase.Status = PurchaseStatus.Canceled;
            try
            {
                ticketsRepository.Update(ticket);
                purchaseRepository.Update(purchase);
                uow.Complete();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditPurchase(Purchase newPurchase)
        {
            var purchase = purchaseRepository.GetById(newPurchase.Id);
            var ticket = ticketsRepository.GetById(purchase.TicketId);
            if (ticket.AvailableTicketsCount < newPurchase.NumberOfTickets) return false;
            purchase.NumberOfTickets = newPurchase.NumberOfTickets;
            try
            {
                purchaseRepository.Update(purchase);
                uow.Complete();
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
                purchaseRepository.Create(purchase);
                uow.Complete();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
