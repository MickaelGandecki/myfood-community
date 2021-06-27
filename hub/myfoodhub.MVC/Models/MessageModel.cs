using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myfoodapp.Hub.Models
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        [Required]
        public DateTime date { get; set; }
        [Required]
        public string content { get; set; }
        [Required]
        public MessageType messageType { get; set; }
        [Required]
        public String device { get; set; }
        public String info { get; set; }
    }

    public class MessageType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

}