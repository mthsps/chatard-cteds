using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace chatard.Models
{
    public class User
    {

        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();
        
        [Required]
        [Index(nameof(Username), IsUnique = true)]
        [StringLength(50)]
        public string Username { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;
        
        [Required]
        [StringLength(50)]
        [Index(nameof(Email), IsUnique = true)]
        
        public string Email { get; set; } = String.Empty;
        
        public string? ProfilePicture { get; set; } = null;
        
        public virtual ICollection<UserContacts> Contacts { get; } = new List<UserContacts>();

        public User() {}
    }

}

