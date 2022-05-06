using System;
using TechTalk.SpecFlow;

namespace WebApi.SpecificationTests.Steps
{
    [Binding]
    public class WeatherStepDefinitions
    {
        [When(@"requesting the weather forecast")]
        public void WhenRequestingTheWeatherForecast()
        {
            throw new PendingStepException();
        }

        [Then(@"we should get the weather forecast")]
        public void ThenWeShouldGetTheWeatherForecast()
        {
            throw new PendingStepException();
        }
    }
}
