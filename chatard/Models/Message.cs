using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatard.Models
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Content { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime SendTime { get; set; } = DateTime.UtcNow;

        public User Sender { get; set; } = new User();

        public User Receiver { get; set; } = new User();

        public Message() { }

    }
}
