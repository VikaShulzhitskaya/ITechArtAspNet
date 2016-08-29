using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Tickets.DAL.Models.Entities
{
    public class User:IdentityUser
    {
        public User()
        {
            Purchases = new HashSet<Purchase>();
        }

        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
