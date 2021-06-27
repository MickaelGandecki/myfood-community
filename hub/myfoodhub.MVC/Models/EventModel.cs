using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models
{
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        [Required]
        public DateTime date { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public ProductionUnit productionUnit { get; set; }
        [Required]
        public EventType eventType { get; set; }

        [Column(TypeName = "text")]
        public string details { get; set; }
        public string createdBy { get; set; }
        public bool? isOpen { get; set; }
        public byte[] picture { get; set; }
    }

    public class EventType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool isDisplayedForUser { get; set; }
        public int order { get; set; }
		public string svgPath { get; set; }
        public List<EventTypeItem> eventTypeItems { get; set; }
    }

    public class EventTypeItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool? isRestrictedForAdmin { get; set; }
        public EventType eventType { get; set; }
        public int order { get; set; }
    }

}