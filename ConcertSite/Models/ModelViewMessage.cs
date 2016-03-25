using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertSite.Models
{
    public class ModelViewMessage
    {
        public Message message { get; set; }
        public List<string> UserListMessage { get; set; }
        public List<Message> myMessage { get; set; }
    }
}