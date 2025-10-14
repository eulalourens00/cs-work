using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace httpclient_web_api
{
    internal class Program
    {
        static HttpClient httpClient = new HttpClient();
        static async  Task Main(string[] args)
        {
            int id = 1;
            using var responce = await httpClient.GetAsync(
                $"https:/localhost:7110/api/users/{id}");
            if (responce.StatusCode == System.Net.HttpStatusCode.NotFound) {
                Error? error = await responce.Content.ReadFromJsonAsync<Error>();
                Console.WriteLine(error?.Message);
            }
            else if(responce.StatusCode == System.Net.HttpStatusCode.OK)
            {
                User? user = await responce.Content.ReadFromJsonAsync<User>();
                Console.WriteLine("{ user?.Name} - {user?.Age}");  
            }

            //List<User>? people = await httpClient.
            //    GetFromJsonAsync<List<User>>("https:/localhost:7110/api/users");
            //if (people != null)
            //{
            //    foreach (var user in people) { Console.WriteLine(user.Name); }
            //}

        }
    }

    record Error(string Message);
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
}
