using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models
{
    public class RecognitionRecordViewModel
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public int productionUnitId { get; set; }
        public ProductionUnitViewModel productionUnit { get; set; }
        public string imagePath { get; set; }
        public string token { get; set; }
        public double dailyProduction { get; set; }
        public int healthLevelId { get; set; }
        public HealthLevelViewModel healthLevel { get; set; }
        public int productionLevelId { get; set; }
        public ProductionLevelViewModel productionLevel { get; set; }
    }

    public class HealthLevelViewModel
    {
        public int Id { get; set; }
        public string name { get; set; }
    }

    public class ProductionLevelViewModel
    {
        public int Id { get; set; }
        public string name { get; set; }
    }
}