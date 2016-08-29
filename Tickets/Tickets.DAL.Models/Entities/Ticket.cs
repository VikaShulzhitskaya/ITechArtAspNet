using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets.DAL.Models.Entities
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long EventId { get; set; }
        public float Price { get; set; }
        public int AvailableTicketsCount { get; set; }
        public DateTime Date { get; set; }

        public virtual Event Event { get; set; }
    }
}
