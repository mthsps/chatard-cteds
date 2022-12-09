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
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public User Contact { get; set; }
    }
}
