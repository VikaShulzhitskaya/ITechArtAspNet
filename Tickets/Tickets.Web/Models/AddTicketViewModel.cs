using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tickets.Web.Models
{
    public class AddTicketViewModel
    {
        [Required]
        public string EventName { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [MaxLength(500)]
        public string EventDescription { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int AvailableCount { get; set; }
    }
}