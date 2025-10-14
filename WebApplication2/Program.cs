
namespace WebApplication2
{
    public class Program
    {
        static int id = 1;
        static List<User> users = new List<User>()
        {
            new(){Id = id++, Name="Asweea", Age = 19},
            new(){Id = id++, Name="Leeky", Age = 21},
            new(){Id = id++, Name="Kleestiana", Age = 17},
        };
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var app = builder.Build();

            app.MapGet("/api/users", () => users);


            // GET
            app.MapGet("/api/users/{id}", (int id) =>
            {
                User? user = users.FirstOrDefault(x => x.Id == id);
                if(user == null)    return Results.NotFound(new {message="Not GET found."});
                return Results.Json(user);
            });
            // DELETE
            app.MapDelete("api/users/{id}", (int id) =>
            {
                User? user = users.FirstOrDefault(x=>x.Id == id);
                if (user == null) return Results.NotFound(new { message = "Not DELETE found" });
                users.Remove(user);
                return Results.Json(user);
            });

            // POST
            app.MapPost("api/users", (User user) =>
            {
                user.Id = id++;
                users.Add(user);
                return user;
            });
            // PUT
            app.MapPut("api/users", (User userData) =>
            {
                var user = users.FirstOrDefault(u => u.Id == userData.Id);
                if (user == null) return Results.NotFound(new { message = "Not PUT found." });
                user.Age = userData.Age;
                user.Name = userData.Name;
                return Results.Json(user);
            });
            app.Run();
        }
    }
}
