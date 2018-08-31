using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreadBuilder.Models
{
    public class Conversions
    {
        public double HydrationLevel(double fWeight, double wWeight)
        {
            double percentage = ((wWeight / fWeight) * 100);

            return Math.Round(percentage);
        }

        /*public double TotalWeight()
        {
            
        }*/


        public double OuncesToLbs(double ounces)
        {
            double lbs = (ounces / 16);

            return lbs;
        }

        public double LbsToOunces(double Lbs)
        {
            double ounces = (Lbs * 16);

            return ounces;
        }

        public double OuncesToGrams(double ounces)
        {
            double grams = (ounces * 28.35);

            return grams;
        }

        public double GramsToOunces(double grams)
        {
            double ounces = (grams / 28.35);

            return ounces;
        }

        public double GramsToKg(double grams)
        {
            double kg = (grams / 1000);

            return kg;
        }

        public double KgToGrams(double kg)
        {
            double grams = (kg * 1000);

            return grams;
        }

        public double TbspToTsp(double tbsp)
        {
            double tsp = (tbsp * 3);

            return tsp;
        }

        public double TspToTbsp(double tsp)
        {
            double tbsp = (tsp / 3);

            return tbsp;
        }

        public double TbspToCup(double tbsp)
        {
            double cups = (tbsp / 16);

            return cups;
        }

        public double CupToTbsp(double cups)
        {
            double tbsp = (cups * 16);

            return tbsp;
        }
    }
}
