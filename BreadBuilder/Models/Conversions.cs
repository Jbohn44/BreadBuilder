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

        public static double DewPoint(string temp, string humidity)
        {
            double tempInF = Convert.ToDouble(temp);
            double humidPerc = Convert.ToDouble(humidity);
            double tempInC = ((tempInF - 32) / 1.8);
            double c1;
            double c2;
            double c3;
            double dewPoint;
            double relHum;
            double relHumInPer = humidPerc / 100;
            double sWVP;
            double pWVP;
            if(tempInC < 0.0)
            {
                c1 = 6.10780;
                c2 = 17.84362;
                c3 = 245.425;

                relHum = relHumInPer / 1;

                sWVP = c1 * Math.Exp((c2 * tempInC) / (c3 + tempInC));
                pWVP = (sWVP * relHum);

                dewPoint = (-Math.Log(pWVP / c1) * c3) / (Math.Log(pWVP / c1) - c2);
                dewPoint = Math.Round((dewPoint * 1.8) + 32);
            }
            else
            {
                c1 = 610780;
                c2 = 17.08085;
                c3 = 234.175;

                relHum = relHumInPer / 1;

                sWVP = c1 * Math.Exp((c2 * tempInC) / (c3 + tempInC));
                pWVP = (sWVP * relHum);

                dewPoint = (-Math.Log(pWVP / c1) * c3) / (Math.Log(pWVP / c1) - c2);
                dewPoint = Math.Round((dewPoint * 1.8) + 32);

            }

            return dewPoint;
        }
    }
}
