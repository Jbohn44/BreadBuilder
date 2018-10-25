using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BreadBuilder.Data;
using BreadBuilder.Models;
using BreadBuilder.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BreadBuilder.Controllers
{
    [Produces("application/json")]
    public class WeatherController : Controller
    {

        private BreadDbContext context;

        public WeatherController(BreadDbContext dbContext)
        {
            context = dbContext;
        }


        public IActionResult Index(int id)
        {
            
            BreadWeatherViewModel viewModel = new BreadWeatherViewModel
            {
                BreadId = id
            };
            return View(viewModel);
        }
      

        [HttpGet]
        public async Task<IActionResult> City(BreadWeatherViewModel breadWeatherViewModel)
        {
            int id = breadWeatherViewModel.BreadId;
            string city = breadWeatherViewModel.City;
            Bread theBread = context.Breads.Single(b => b.ID == id);
            List<RecipeItem> items = context.RecipeItems.Include(i => i.RecipeIngredient).Include(y => y.RecipeMeasurement).Where(x => x.Bread.ID == id).ToList();

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid=47a5c131dcaf6e49c667eac297513dd8&units=imperial");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);

                    double dewPoint = Conversions.DewPoint(rawWeather.Main.Temp, rawWeather.Main.Humidity);

                    double hydration = Conversions.HydrationLevel(items);

                    List<double> totalWeights = Conversions.TotalWeights(items);

                    WeatherViewModel weatherViewModel = new WeatherViewModel
                    {
                        City = rawWeather.Name,
                        Temp = rawWeather.Main.Temp,
                        Humidity = rawWeather.Main.Humidity,
                        BreadID = breadWeatherViewModel.BreadId,
                        DewPoint = dewPoint,
                        Bread = theBread,
                        Items = items,
                        Hydration = hydration,
                        TotalWeights = totalWeights

                    };

                    WeatherViewModel viewModel = Conversions.WeatherAdjustment(weatherViewModel);
                    TempData.Keep();

                    return View("ViewWeather", viewModel);




                   /* return Ok(new
                    {
                        Temp = rawWeather.Main.Temp,
                        Humidity = rawWeather.Main.Humidity,
                        Summary = string.Join(",", rawWeather.Weather.Select(x => x.Main)),
                        City = rawWeather.Name
                    }); */
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
                }
            }
        }

    }
}