using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DemoApiClient
{
    public class Program
    {
        public static Task Main(string[] args) => CreateHostBuilder(args).Build().RunAsync();

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            IHostBuilder hostBuilder = new HostBuilder()
                .ConfigureServices(services =>
                {
                    //Register all the services for dependency injection
                    services.AddLogging();
                });

            return hostBuilder;
        }
    }
}