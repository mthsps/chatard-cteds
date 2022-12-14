using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatard.Models
{
    public class UserContacts
    {

        public int Id { get; set; }
        public Guid ContactId { get; set; }
        public Guid UserId { get; set; }

        public virtual User User { get; set; }
        public virtual User Contact { get; set; }
    }
}