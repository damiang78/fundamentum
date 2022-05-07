using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace WebApi.SpecificationTests.Steps
{
    [Binding]
    public class WeatherStepDefinitions
    {
        private readonly HttpClient _httpClient;
        private readonly ScenarioContext _scenarioContext;

        public WeatherStepDefinitions(HttpClient httpClient, ScenarioContext scenarioContext)
        {
            _httpClient = httpClient;
            _scenarioContext = scenarioContext;
        }

        [When(@"requesting the weather forecast")]
        public async Task WhenRequestingTheWeatherForecast()
        {
            var response = await _httpClient.GetAsync("weatherforecast");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var forecast = JsonConvert.DeserializeObject<List<WeatherForecast>>(content);

            _scenarioContext.Add("WeatherForecast", forecast);
        }

        [Then(@"we should get the weather forecast")]
        public void ThenWeShouldGetTheWeatherForecast()
        {
            var forecast = _scenarioContext.Get<List<WeatherForecast>>("WeatherForecast");

            forecast.Should().NotBeNull();
            forecast.Should().NotBeEmpty();
        }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
