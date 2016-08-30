using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tickets.Web.Models
{
    public class IndexEventViewModel
    {
        public List<EventViewModel> EventViewModels { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}