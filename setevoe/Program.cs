using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace setevoe
{
    internal class Program
    {
        static HttpClient httpClient = new HttpClient();
        static async Task Main(string[] args)
        {
            //using HttpRequestMessage request = new HttpRequestMessage(
            //    HttpMethod.Get, "https://ya.ru");
            ////await HttpClient.SendAsync(request);

            //using HttpResponseMessage response = await HttpClient.SendAsync(request);

            //Console.WriteLine($"status: {response.StatusCode}\n");
            //Console.WriteLine("headers");

            //foreach( var header in response.Headers)
            //{
            //    Console.Write($"{header.Key}");
            //    foreach(var headerValue in header.Value)
            //    {  Console.WriteLine(headerValue);}
            //}

            //Console.WriteLine($"\nContent");
            //string content = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(content);

            //using HttpResponseMessage response = await httpClient.GetAsync(
            //    "https://ya.ru");
            //string content = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(content);

            //string content= await httpClient.GetStringAsync("https://ya.ru");
            //Console.WriteLine(content);

            //byte[] buffer = await httpClient.GetByteArrayAsync("https://ya.ru");
            //Console.WriteLine(Encoding.UTF8.GetString(buffer));

            //using Stream stream = await httpClient.GetStreamAsync("https://ya.ru");
            //StreamReader reader = new StreamReader(stream);

            //string content = await reader.ReadToEndAsync();
            //Console.WriteLine(content);

            //User? user = await httpClient.GetFromJsonAsync<User>("https://localhost7245/");
            //Console.WriteLine($"{user?.name}, {user?.age}");


            using var response = await httpClient.GetAsync("http://localhost:5294");
            if (response.StatusCode == HttpStatusCode.BadRequest ||
                response.StatusCode == HttpStatusCode.NotFound)
            {
                Error? error = await response.Content.ReadFromJsonAsync<Error>();
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(error?.Message);

            }
            else{
                User? user = await response.Content.ReadFromJsonAsync<User>();
                Console.WriteLine($"name - {user?.name}, age - {user?.age}");
            }
        }
        record User(string name, int age);
        record Error(string Message);
    }
}