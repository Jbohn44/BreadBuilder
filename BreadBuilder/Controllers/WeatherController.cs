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
            string city = breadWeatherViewModel.City;
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("http://api.openweathermap.org");
                    var response = await client.GetAsync($"/data/2.5/weather?q={city}&appid=47a5c131dcaf6e49c667eac297513dd8&units=imperial");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);

                    WeatherViewModel weatherViewModel = new WeatherViewModel
                    {
                        City = rawWeather.Name,
                        Temp = rawWeather.Main.Temp,
                        Humidity = rawWeather.Main.Humidity,
                        BreadID = breadWeatherViewModel.BreadId
                    };

                    return View("ViewWeather", weatherViewModel);
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