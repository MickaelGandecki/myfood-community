using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models
{
    public class EventViewModel
    {
        public Int64 Id { get; set; }
        public DateTime date { get; set; }
        public string description { get; set; }
        public int productionUnitId { get; set; }
        public ProductionUnitViewModel productionUnit { get; set; }
        public int eventTypeId { get; set; }
        public EventTypeViewModel eventType { get; set; }
        public string details { get; set; }
        public bool? isOpen { get; set; }
        public string createdBy { get; set; }
        public byte[] picture { get; set; }
    }

    public class EventTypeViewModel
    {
        public int Id { get; set; }
        public string name { get; set; }
    }

}