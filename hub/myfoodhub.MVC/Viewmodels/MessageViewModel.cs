using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models
{
    public class MessageViewModel
    {
        public Int64 Id { get; set; }
        public DateTime date { get; set; }
        public string content { get; set; }
        public int messageTypeId { get; set; }
        public MessageTypeViewModel messageType { get; set; }
        public String device { get; set; }
        public String info { get; set; }
    }

}