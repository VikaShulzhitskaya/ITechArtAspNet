using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tickets.Web.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}