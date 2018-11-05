using BreadBuilder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreadBuilder.Models
{
    public static class Conversions
    {
        public static double HydrationLevel(List<RecipeItem> items)
        {
            double flourValue = 0;
            double waterValue = 0;

            //This is used to calculate the recipe's hydration by finding the ingredients that contain certain keywords... needs to be converted to a method and stored in Conversions model
            foreach (var i in items)
            {
                if (KeyWordLists.Flours.Contains(i.RecipeIngredient.Name.ToLower()))
                {
                    flourValue += i.RecipeMeasurement.Value;
                }
                if (KeyWordLists.Liquids.Contains(i.RecipeIngredient.Name.ToLower()))
                {
                    waterValue += i.RecipeMeasurement.Value;
                }
            }
            double percentage = ((waterValue / flourValue) * 100);

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

        public static List<string> ExistingUsers(List<User> users)
        {
            List<string> existingUsers = new List<string>();
            foreach (var user in users)
            {
                existingUsers.Add(user.Name);
            }

            return existingUsers;
        }

        public static List<RecipeItem> ConvertItemsToGrams(List<RecipeItem> items)
        {
            foreach (var i in items)
            {
                if (i.RecipeMeasurement.Unit == MeasurementUnit.oz)
                {
                    i.RecipeMeasurement.Unit = MeasurementUnit.g;
                    i.RecipeMeasurement.Value = Conversions.OuncesToGrams(i.RecipeMeasurement.Value);
                }
            }

            return items;
        }

        public static List<RecipeItem> ConvertItemsToOz(List<RecipeItem> items)
        {
            foreach (var i in items)
            {
                if (i.RecipeMeasurement.Unit == MeasurementUnit.g)
                {
                    i.RecipeMeasurement.Unit = MeasurementUnit.oz;
                    i.RecipeMeasurement.Value = Conversions.GramsToOunces(i.RecipeMeasurement.Value);
                }
            }

            return items;
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

        public static WeatherViewModel WeatherAdjustment(WeatherViewModel weatherViewModel)
        {
            Double dewPoint = weatherViewModel.DewPoint;
            List<RecipeItem> items = weatherViewModel.Items.ToList();

           if(dewPoint <= 20)
            {
                foreach(var i in items)
                {
                    if (KeyWordLists.Liquids.Contains(i.RecipeIngredient.Name.ToLower()))
                    {
                        i.RecipeMeasurement.Value = (i.RecipeMeasurement.Value) + (i.RecipeMeasurement.Value * .10);
                        weatherViewModel.AdjustedLiquid = i.RecipeIngredient.Name;
                        weatherViewModel.AdjustmentMessage = $"was added to {i.RecipeIngredient.Name} weight.";
                        weatherViewModel.AdjustmentPercent = "10 % ";
                    }
                }
            }
           else if(dewPoint >= 21 && dewPoint <= 40)
            {
                foreach (var i in items)
                {
                    if (KeyWordLists.Liquids.Contains(i.RecipeIngredient.Name.ToLower()))
                    {
                        i.RecipeMeasurement.Value = (i.RecipeMeasurement.Value) + (i.RecipeMeasurement.Value * .05);
                        weatherViewModel.AdjustedLiquid = i.RecipeIngredient.Name;
                        weatherViewModel.AdjustmentMessage = $"was added to {i.RecipeIngredient.Name} weight.";
                        weatherViewModel.AdjustmentPercent = "5 % ";
                    }
                }
            }
           else if(dewPoint >= 41 && dewPoint <= 60)
            {
                foreach (var i in items)
                {
                    if (KeyWordLists.Liquids.Contains(i.RecipeIngredient.Name.ToLower()))
                    {
                        i.RecipeMeasurement.Value = i.RecipeMeasurement.Value;
                        weatherViewModel.AdjustmentMessage = "No adjustments made.";
                    }
                }
            }
           else if(dewPoint >= 61 && dewPoint <= 70)
            {
                foreach (var i in items)
                {
                    if (KeyWordLists.Liquids.Contains(i.RecipeIngredient.Name.ToLower()))
                    {
                        i.RecipeMeasurement.Value = (i.RecipeMeasurement.Value) - (i.RecipeMeasurement.Value * .05);
                        weatherViewModel.AdjustedLiquid = i.RecipeIngredient.Name;
                        weatherViewModel.AdjustmentMessage = $"was subtracted from {i.RecipeIngredient.Name} weight.";
                        weatherViewModel.AdjustmentPercent = "5 % ";
                    }
                }
            }
           else if(dewPoint >= 71 && dewPoint <= 80)
            {
                foreach (var i in items)
                {
                    if (KeyWordLists.Liquids.Contains(i.RecipeIngredient.Name.ToLower()))
                    {
                        i.RecipeMeasurement.Value = (i.RecipeMeasurement.Value) - (i.RecipeMeasurement.Value * .10);
                        weatherViewModel.AdjustedLiquid = i.RecipeIngredient.Name;
                        weatherViewModel.AdjustmentMessage = $"was subtracted from {i.RecipeIngredient.Name} weight.";
                        weatherViewModel.AdjustmentPercent = "10 % ";
                    }
                }
            }
            else
            {
                foreach (var i in items)
                {
                    if (KeyWordLists.Liquids.Contains(i.RecipeIngredient.Name.ToLower()))
                    {
                        i.RecipeMeasurement.Value = (i.RecipeMeasurement.Value) - (i.RecipeMeasurement.Value * .15);
                        weatherViewModel.AdjustedLiquid = i.RecipeIngredient.Name;
                        weatherViewModel.AdjustmentMessage = $"was subtracted from {i.RecipeIngredient.Name} weight.";
                        weatherViewModel.AdjustmentPercent = "15 % ";
                    }
                }
            }



            double hydration = Conversions.HydrationLevel(items);

            List<double> totalWeights = Conversions.TotalWeights(items);

            WeatherViewModel viewModel = new WeatherViewModel
            {
                City = weatherViewModel.City,
                Temp = weatherViewModel.Temp,
                Humidity = weatherViewModel.Humidity,
                DewPoint = weatherViewModel.DewPoint,
                BreadID = weatherViewModel.BreadID,
                Bread = weatherViewModel.Bread,
                Items = items,
                Hydration = hydration,
                TotalWeights = totalWeights,
                AdjustedLiquid = weatherViewModel.AdjustedLiquid,
                AdjustmentMessage = weatherViewModel.AdjustmentMessage,
                AdjustmentPercent = weatherViewModel.AdjustmentPercent

            };
            return viewModel;
        }
    }
}
