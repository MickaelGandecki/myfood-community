using myfoodapp.Hub.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models
{
    public class MeasureViewModel
    {
        public Int64 Id { get; set; }
        public DateTime captureDate { get; set; }
        public decimal value { get; set; }
        public SensorTypeViewModel sensor { get; set; }
        public int sensorId { get; set; }
        public int productionUnitId { get; set; }
        public ProductionUnitViewModel productionUnit { get; set; }
    }

    public class SensorTypeViewModel
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime? lastCalibration { get; set; }
    }

    public class GroupedMeasureViewModel
    {
        public Int64 Id { get; set; }
        public DateTime captureDate { get; set; }
        public decimal airTemperatureValue { get; set; }
        public decimal humidityValue { get; set; }
        public decimal externalAirTemperatureValue { get; set; }
        public decimal externalHumidityValue { get; set; }

    }

    public class Rule
    {
        public string ruleEvaluator { get; set; }
        public int impactLevel { get; set; }
        public string title { get; set; }
        public string titleFR { get; set; }
        public string titleDE { get; set; }
        public string description { get; set; }
        public string descriptionFR { get; set; }
        public string descriptionDE { get; set; }
        public string url { get; set; }
        public string bindingPropertyValue { get; set; }
    }
}