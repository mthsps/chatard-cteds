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

        public int Id { get; set; }
        public string Content { get; set; }

        public DateTime SendTime { get; set; }
        public String Sender { get ; set; }
        
        public String Receiver { get ; set; }

        public Message() { }

    }
}
