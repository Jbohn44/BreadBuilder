using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreadBuilder.Models
{
    public class Conversions
    {
        public static double HydrationLevel(double fWeight, double wWeight)
        {
            double percentage = ((wWeight / fWeight) * 100);

            return Math.Round(percentage);
        }

        public static List<double> TotalWeights(List<RecipeItem> items)
        {
            List<double> totals = new List<double>();
            double totalOz = 0;
            double totalG = 0;


            foreach (var i in items)
            {
                if (i.RecipeMeasurement.Unit == MeasurementUnit.oz)
                {
                    totalOz = (totalOz + i.RecipeMeasurement.Value);


                }
                else if(i.RecipeMeasurement.Unit == MeasurementUnit.g)
                {
                    totalG = (totalG + i.RecipeMeasurement.Value);
                }


            }
            totals.Add(totalOz);
            totals.Add(totalG);

            return totals;
        }


        public static double OuncesToLbs(double ounces)
        {
            double lbs = (ounces / 16);

            return lbs;
        }

        public static double LbsToOunces(double Lbs)
        {
            double ounces = (Lbs * 16);

            return ounces;
        }

        public static double OuncesToGrams(double ounces)
        {
            double grams = (ounces * 28.35);

            return grams;
        }

        public static double GramsToOunces(double grams)
        {
            double ounces = (grams / 28.35);

            return ounces;
        }

        public static double GramsToKg(double grams)
        {
            double kg = (grams / 1000);

            return kg;
        }

        public static double KgToGrams(double kg)
        {
            double grams = (kg * 1000);

            return grams;
        }

        public static double TbspToTsp(double tbsp)
        {
            double tsp = (tbsp * 3);

            return tsp;
        }

        public static double TspToTbsp(double tsp)
        {
            double tbsp = (tsp / 3);

            return tbsp;
        }

        public static double TbspToCup(double tbsp)
        {
            double cups = (tbsp / 16);

            return cups;
        }

        public static double CupToTbsp(double cups)
        {
            double tbsp = (cups * 16);

            return tbsp;
        }
    }
}
