using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models
{
    public class Measure
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        [Required]
        public DateTime captureDate { get; set; }
        [Required]
        public decimal value { get; set; }
        [Required]
        public SensorType sensor { get; set; }
        [Required]
        public ProductionUnit productionUnit { get; set; }
    }

    public class SensorType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
        public string description { get; set; }
        public DateTime? lastCalibration { get; set; }
    }

    public enum SensorTypeEnum
    {
        ph = 1,
        waterTemperature = 2,
        dissolvedOxygen = 3,
        orp = 4,
        airTemperature = 5,
        humidity = 6,
        externalAirTemperature = 7,
        externalHumidity = 8,
        airCarbonDioxide = 9
    }
}