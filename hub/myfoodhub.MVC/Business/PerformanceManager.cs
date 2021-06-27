using myfoodapp.Hub.Models;
using myfoodapp.Hub.Models.OpenData;
using myfoodapp.Hub.Services;
using System;
using System.Data.Entity;
using System.Linq;

namespace myfoodapp.Hub.Business
{
    public static class PerformanceManager
    {
        private static int monthlyAverageProductionAerospring = 4;
        private static int monthlyAverageProductionCity = 7;
        private static int monthlyAverageProductionFamilyWithoutBeds = 12;
        private static int monthlyAverageProductionFamilyWithBeds = 15;
        private static int monthlyAverageProductionFarm = 25;

        //CARLSSON-KANYAMA, Annika, et al. "Potential contributions of food consumption patterns to climate change."
        // The American journal of clinical nutrition. 2009, 1704S-1709S
        private static double CO2SparedPerKilogramLocallyProduced = 1.1;

        public static OpenProductionUnitsStatsViewModel GetNetworkStatistics(ApplicationDbContext db)
        {
            MeasureService measureService = new MeasureService(db);

            var productionUnits = db.ProductionUnits;

            var productionUnitNumber = 0;

            var totalBalcony = productionUnits.Where(p => p.productionUnitType.Id == 1).Count();

            var totalCity = productionUnits.Where(p => p.productionUnitType.Id == 2 ||
                                            p.productionUnitType.Id == 12).Count();

            var totalFamilyWithoutBeds = productionUnits.Where(p => p.productionUnitType.Id == 3 ||
                                                         p.productionUnitType.Id == 4).Count();

            var totalFamilyWithBeds = productionUnits.Where(p => p.productionUnitType.Id == 8 ||
                                                      p.productionUnitType.Id == 9 ||
                                                      p.productionUnitType.Id == 10 ||
                                                      p.productionUnitType.Id == 11).Count();

            var totalFarm = productionUnits.Where(p => p.productionUnitType.Id == 5).Count();

            var totalMonthlyProduction = totalBalcony * monthlyAverageProductionAerospring
                                           + totalCity * monthlyAverageProductionCity
                                           + totalFamilyWithoutBeds * monthlyAverageProductionFamilyWithoutBeds
                                           + totalFamilyWithBeds * monthlyAverageProductionFamilyWithoutBeds
                                           + totalFarm * monthlyAverageProductionFarm;

            var totalMonthlySparedCO2 = Math.Round(totalMonthlyProduction * CO2SparedPerKilogramLocallyProduced);

            productionUnitNumber = productionUnits.Count();

            return new OpenProductionUnitsStatsViewModel()
            {
                productionUnitNumber = productionUnitNumber,
                totalMonthlyProduction = totalMonthlyProduction,
                totalMonthlySparedCO2 = totalMonthlySparedCO2
            };
        }

        public static OpenProductionUnitsStatsViewModel GetNetworkStatisticsAndIncidents(ApplicationDbContext db)
        {
            MeasureService measureService = new MeasureService(db);

            var productionUnits = db.ProductionUnits;

            var productionUnitNumber = 0;
            var totalMonthlyIncident = 0;
            var totalAnnuallyIncident = 0;

            productionUnitNumber = productionUnits.Where(p => p.productionUnitStatus.Id != 5).Count();

            var lastMonth = DateTime.Now.AddMonths(-1);
            var lastYear = DateTime.Now.AddMonths(-1);

            totalMonthlyIncident = db.Events.Where(e => e.eventType.Id == 2 && e.date > lastMonth && e.createdBy != "Myfood Bot").Count();
            totalAnnuallyIncident = db.Events.Where(e => e.eventType.Id == 2 && e.date > lastYear && e.createdBy != "Myfood Bot").Count();

            return new OpenProductionUnitsStatsViewModel()
            {
                productionUnitNumber = productionUnitNumber,
                totalMonthlyIncident = totalMonthlyIncident,
                totalAnnuallyIncident = totalAnnuallyIncident            
            };
        }

        public static string GetConnectivityStatistics(ApplicationDbContext db)
        {
            MeasureService measureService = new MeasureService(db);

            var productionUnits = db.ProductionUnits.Include(p => p.productionUnitStatus).ToList();

            var lastYearUpPercent = 0.0;
            var twoYearsUpPercent = 0.0;
            var threeYearsUpPercent = 0.0;

            var upLastYear = 0.0;
            var upTwoYearsAgo = 0.0;
            var upThreeYearsAgo = 0.0;

            var downLastYear = 0.0;
            var downTwoYearsAgo = 0.0;
            var downThreeYearsAgo = 0.0;

            var lastYearDate = DateTime.Today.AddYears(-1);
            var twoYearsAgoDate = DateTime.Today.AddYears(-2);
            var threeYearsAgoDate =  DateTime.Today.AddYears(-3);

            var lastYear = productionUnits.Where(p => p.startDate > lastYearDate).ToList();

            if (lastYear != null)
            {
                upLastYear = lastYear.Where(p => p.productionUnitStatus.Id == 3).Count();
                downLastYear = lastYear.Where(p => p.productionUnitStatus.Id == 6).Count();

                if (upLastYear + downLastYear > 0)
                    lastYearUpPercent = 100 * (upLastYear / (upLastYear + downLastYear));
            }

            var twoYearsAgo = productionUnits.Where(p => p.startDate > twoYearsAgoDate && p.startDate < lastYearDate).ToList();

            if(twoYearsAgo != null)
            {
                upTwoYearsAgo = twoYearsAgo.Where(p => p.productionUnitStatus.Id == 3).Count();
                downTwoYearsAgo = twoYearsAgo.Where(p => p.productionUnitStatus.Id == 6).Count();

                if (upTwoYearsAgo + downTwoYearsAgo > 0)
                    twoYearsUpPercent = 100 * (upTwoYearsAgo / (upTwoYearsAgo + downTwoYearsAgo));
            }
                       
            var threeYearsAgo = productionUnits.Where(p => p.startDate < twoYearsAgoDate).ToList();

            if(threeYearsAgo != null)
            {
                upThreeYearsAgo = threeYearsAgo.Where(p => p.productionUnitStatus.Id == 3).Count();
                downThreeYearsAgo = threeYearsAgo.Where(p => p.productionUnitStatus.Id == 6).Count();

                if (upThreeYearsAgo + downThreeYearsAgo > 0)
                    threeYearsUpPercent = 100 * (upThreeYearsAgo / (upThreeYearsAgo + downThreeYearsAgo));
            }

            return String.Format("{0}%(3y+) - {1}%(2y) - {2}%(1y)", Math.Round(threeYearsUpPercent, 0), 
                                                       Math.Round(twoYearsUpPercent, 0), 
                                                       Math.Round(lastYearUpPercent, 0)); 
        }

        public static int GetEstimatedMonthlyProduction(int productionUnitTypeId)
        {
            var averageMonthlyProduction = 0;

            switch (productionUnitTypeId)
            {
                case 1:
                    //AeroSpring
                    averageMonthlyProduction = monthlyAverageProductionAerospring;
                    break;
                case 2:
                case 12:
                    //City Original & Signature
                    averageMonthlyProduction = monthlyAverageProductionCity;
                    break;
                case 3:
                case 4:
                    //Family14 & Family22 Original
                    averageMonthlyProduction = monthlyAverageProductionFamilyWithoutBeds;
                    break;
                case 8:
                case 9:
                case 10:
                case 11:
                    //Family22 Production -> Signature
                    averageMonthlyProduction = monthlyAverageProductionFamilyWithBeds;
                    break;
                case 5:
                    //Farm
                    averageMonthlyProduction = monthlyAverageProductionFarm;
                    break;
                default:
                    break;
            }

            return averageMonthlyProduction;
        }

        public static double GetEstimatedMonthlySparedCO2(int averageMonthlyProduction)
        {
            return Math.Round(averageMonthlyProduction * CO2SparedPerKilogramLocallyProduced, 0);
        }

        public static decimal GetNetworkScore(ApplicationDbContext db)
        {
            decimal networkScore = 0;

            var upAndRunning = db.ProductionUnits.Where(p => p.productionUnitStatus.Id == 3).Count();

            var lastMeasures = db.Measures.Where(m => m.sensor.Id == 1).OrderByDescending(m => m.captureDate).Take(upAndRunning);

            lastMeasures.ToList().ForEach(m => {
                networkScore += Math.Abs(m.value - 6.8m);
            });

            if (upAndRunning > 0)
                return Math.Round(networkScore, 1);
            else
                return 0;
        }
    }
}