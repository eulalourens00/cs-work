
namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            //app.MapGet("/", () => new User("Ivan", 20));

            app.MapGet("/{id}", (int? id) =>
            {
                if(id is null) { return Results.BadRequest(new { Message = "Uncorrect data in request" }); }

                else if (id != 1) { return Results.NotFound(new { Message = "Not alive" }); }

                else  return Results.Json(new User("Vova", 19));
            });



            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.Run();
        }
        record User(string name, int age);
    }
}
