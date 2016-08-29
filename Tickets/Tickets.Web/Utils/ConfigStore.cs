using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Tickets.Web.Utils
{
    public static class ConfigStore
    {
        public static string GetConnectionString()
        {
            return WebConfigurationManager.AppSettings["CinemaTickets"];
        }
    }
}