using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models.OpenData
{
    public class OpenProductionUnitViewModel
    {
        public int Id { get; set; }
        public DateTime? startDate { get; set; }
        public string productionUnitType { get; set; }
        public string hydroponicType { get; set; }
        public string productionUnitStatus { get; set; }
        public double locationLatitude { get; set; }
        public double locationLongitude { get; set; }
        public String info { get; set; }
        public String version { get; set; }
        public DateTime? lastMeasureReceived { get; set; }
        public string pioneerCitizen { get; set; }
        public int pioneerCitizenNumber { get; set; }
    }

    public class SecureProductionUnitViewModel : OpenProductionUnitViewModel
    {
        public string picturePath { get; set; }
        public string pionnerCitizenOffice365Account { get; set; }

        public IList<Option> options { get; set; }
    }


    public class OpenOptionViewModel
    {
        public int Id { get; set; }
        public string name { get; set; }
    }

    public class OpenProductionUnitsStatsViewModel
    {
        public int productionUnitNumber { get; set; }
        public int totalMonthlyProduction { get; set; }
        public double totalMonthlySparedCO2 { get; set; }
        public int totalMonthlyIncident { get; set; }
        public int totalAnnuallyIncident { get; set; }

    }
}