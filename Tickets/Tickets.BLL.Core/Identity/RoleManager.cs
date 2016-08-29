using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Tickets.DAL.Models.Entities;

namespace Tickets.BLL.Core.Identity
{
    public class RoleManager : RoleManager<Role>
    {
        public RoleManager(IRoleStore<Role> store) : base(store)
        {
        }
    }
}
