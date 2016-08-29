using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets.DAL.Models.Enums;

namespace Tickets.DAL.Models.Entities
{
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long TicketId { get; set; }
        public string UserId { get; set; }
        public int NumberOfTickets { get; set; }
        public PurchaseStatus Status { get; set; }
        public virtual User User { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
