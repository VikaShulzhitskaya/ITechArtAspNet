using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Tickets.DAL.Models.Entities;

namespace Tickets.DAL.Core
{
    public class UserStore : UserStore<User>
    {
        public UserStore(DbContext context) : base(context)
        {
            
        }
    }
}
