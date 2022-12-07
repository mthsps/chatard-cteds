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
        public string Content { get; set; }

        public DateTime SendTime { get; set; }
        
        public User Sender { get ; set; }
        
        public User Receiver { get ; set; }

        public Message() { }

    }
}
