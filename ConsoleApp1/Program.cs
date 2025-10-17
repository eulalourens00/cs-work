using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            //Dictionary<string, string> data = new Dictionary<string, string>()
            //{
            //    ["name"] = "Asweea",
            //    ["email"] = "asweea19@mail.ru",
            //    ["age"] = "19"
            //};

            //HttpContent httpContent = new FormUrlEncodedContent(data);
            //using var response = await client.PostAsync("http://localhost:5154/data", httpContent);
            //string resText = await response.Content.ReadAsStringAsync();

            //Console.WriteLine(resText);

            //string filePath = "C://cs-work//cs-work//ClothingStoreApp//Resources//Images//main.png";
            //using var fileStream = File.OpenRead(filePath);
            //StreamContent content = new StreamContent(fileStream);
            //using var response = await client.PostAsync("http://localhost:5154/data", content);
            //string resText = await response.Content.ReadAsStringAsync();


            HttpClient client = new HttpClient();
            string mes = "EREN YAGER";
            byte[]mesToBytes = Encoding.UTF8.GetBytes(mes);
            var content = new ByteArrayContent(mesToBytes);

            using var response = await client.PostAsync("http://localhost:5154/data", content);

            string resText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(resText);
        }
    }
}
