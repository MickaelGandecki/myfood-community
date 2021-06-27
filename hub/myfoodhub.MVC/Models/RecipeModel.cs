using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models
{
    public class Recipes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }
        public string advice { get; set; }
        [Required]
        public GardeningType gardeningType { get; set; }
        public int daysOfGermination { get; set; }
        public int daysOfHarvestFromSowing { get; set; }
        public List<Month> plantingIndoorMonths { get; set; }
        public List<Month> plantingOutdoorMonths { get; set; }
        public List<Month> harvestingMonths { get; set; }
        public int minimumSpaceBetweenPlantInTower { get; set; }
        public int proteinPercentage { get; set; }
        public WateringLevel wateringLevel { get; set; }
        [Required]
        public decimal idealMinTemperature { get; set; }
        [Required]
        public decimal idealMaxTemperature { get; set; }
        [Required]
        public decimal acceptableMaxTemperature { get; set; }
        [Required]
        public decimal acceptableMinTemperature { get; set; }
        [Required]
        public string reference { get; set; }
        [Required]
        public string picturePath { get; set; }
        [NotMapped]
        public bool isRecommended { get; set; }
    }


    public class GardeningType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
        public string description { get; set; }
        public string imagePath { get; set; }
    }

    public class WateringLevel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
        public string description { get; set; }
        public string imagePath { get; set; }
    }

    public class Month
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int order { get; set; }
        [Required]
        public Season season { get; set; }

    }
    public class Season
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        public string name { get; set; }

    }

}