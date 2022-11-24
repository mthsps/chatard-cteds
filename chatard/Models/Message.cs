using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatard.Models
{
    public class Message
    {
        public string content { get; set; }

        public DateTime sendTime { get; set; }

        public User sender { get ; set; }

        public User receiver { get ; set; }

        public Message() { }

    }
}
