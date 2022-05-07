using BoDi;
using Ductus.FluentDocker.Services;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace WebApi.SpecificationTests.Hooks
{
    [Binding]
    public class DockerControllerHooks
    {
        private static ICompositeService _compositeService;
        private readonly IObjectContainer _objectContainer;

        public DockerControllerHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void DockerComposeUp()
        {
            var config = LoadConfiguration();

            var dockerComposeFilename = config["DockerComposeFilename"];
            var dockerComposePath = GetDockerComposeLocation(dockerComposeFilename);

            var confirmationUrl = config["Weather.Api:BaseAddress"];
            _compositeService = new Ductus.FluentDocker.Builders.Builder()
                .UseContainer()
                .UseCompose()
                .FromFile(dockerComposePath)
                .RemoveOrphans()
                .WaitForHttp("webapi", $"{confirmationUrl}/weatherforecast", 
                    continuation: (response, _) => response.Code != System.Net.HttpStatusCode.OK ? 2000 : 0)
                .Build().Start();
        }

        [AfterTestRun]
        public static void DockerComposeDown()
        {
            _compositeService.Stop();
            _compositeService.Dispose();
        }

        [BeforeScenario]
        public void AddHttpClient()
        {
            var config = LoadConfiguration();
            var httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(config["Weather.Api:BaseAddress"])
            };
            _objectContainer.RegisterInstanceAs(httpClient);
        }

        private static IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appSettings.json")
                .Build();
        }

        private static string GetDockerComposeLocation(string dockerComposeFilename)
        {
            var directory = Directory.GetCurrentDirectory();
            while (!Directory.EnumerateFiles(directory, "*.yml").Any(s => s.EndsWith(dockerComposeFilename)))
            {
                directory = directory.Substring(0, directory.LastIndexOf(Path.DirectorySeparatorChar));
            }

            return Path.Combine(directory, dockerComposeFilename);
        }
    }
}
