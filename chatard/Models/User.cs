using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatard.Models
{
    public class User
    {

        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string ProfilePicture { get; set; } = String.Empty;
        public virtual ICollection<UserContacts> Contacts { get; } = new List<UserContacts>();

        public User() {}
    }

}

