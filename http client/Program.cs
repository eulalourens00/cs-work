using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace http_client
{
    internal class Program
    {
        static HttpClient? httpClient;
        static async Task Main(string[] args)
        {
            //OPTIONS
            //TRACE
            //CONNECT
            //GET
            //HEAD
            //POST

            HttpMethod customMethod = new HttpMethod("Custom");

            //GET/ users/id/12 HTTP/1.1

            //\r\n
            //var message = "GET / HTTP/1.1\r\n" +
            //    "Host: www.google.com\r\n" +
            //    "Conection: Close \r\n\r\n";

            //HttpMessageHandler handler = new HttpClientHandler();
            //Console.WriteLine("Work...");
            //for(int i = 0; i < 10; i++)
            //{
            //    using (var client = new HttpClient(handler, false))
            //    {
            //        using var res = await client.GetAsync("https://google.com");
            //        Console.WriteLine(res.StatusCode);
            //    }
            //}
            //Console.WriteLine("End work...");

            //var socketHandler = new SocketsHttpHandler
            //{
            //    PooledConnectionIdleTimeout = TimeSpan.FromSeconds(5)
            //};
            //httpClient = new HttpClient(socketHandler);

            //for(int i = 0; i < 10; i++)
            //{
            //    using var res = await httpClient.GetAsync(
            //        "https://google.com");
            //    Console.WriteLine(res.StatusCode);
            //}

            var services = new ServiceCollection();
            services.AddHttpClient();
            var servicesProvider = services.BuildServiceProvider();
            var httpClientFactory = servicesProvider.GetService<IHttpClientFactory>();
            var httpClient = httpClientFactory?.CreateClient();

            for (int i = 0; i < 10; i++)
            {
                using var res = await httpClient.GetAsync(
                    "https://google.com");
                Console.WriteLine(res.StatusCode);
            }
        }
    }
}
