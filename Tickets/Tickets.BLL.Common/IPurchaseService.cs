using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.DAL.Models.Entities;

namespace Tickets.BLL.Common
{
    public interface IPurchaseService
    {
        IEnumerable<Purchase> GetPurchasesByUserId(string id);
        IEnumerable<Purchase> GetUnconfirmedPurchasesByUserId(string id);
        Purchase GetPurchase(long id);
        bool ConfirmPurchase(long id);
        bool CancelPurchase(long id);
        bool EditPurchase(Purchase purchase);
        bool AddPurchase(Purchase purchase);
    }
}
