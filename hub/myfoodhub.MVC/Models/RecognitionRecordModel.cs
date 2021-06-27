using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models
{
        public class RecognitionRecord
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }
            [Required]
            public DateTime date { get; set; }
            [Required]
            public ProductionUnit productionUnit { get; set; }
            [Required]
            public string imagePath { get; set; }
            [Required]
            public string token { get; set; }
            [Required]
            public double dailyProduction { get; set; }
            [Required]
            public HealthLevel healthLevel { get; set; }
            [Required]
            public ProductionLevel productionLevel { get; set; }
        }

        public class HealthLevel
        {
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int Id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
        }

        public class ProductionLevel
        {
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            public int Id { get; set; }
            public string name { get; set; }
            public string description { get; set; }
        }

}