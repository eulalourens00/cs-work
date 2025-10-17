namespace cry_and_cry
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            //app.MapPost("/data", async (HttpContext httpContext) =>
            //{
            //    var form = httpContext.Request.Form;
            //    string? name = form["name"];
            //    string? email = form["email"];
            //    string? age = form["age"];

            //    await httpContext.Response.WriteAsync($"Name - {name}\n" +
            //        $"Email - {email}\nAge - {age}");
            //});

            //app.MapPost("/data", async (HttpContext httpContext) =>
            //{
            //    var upPath = $"{Directory.GetCurrentDirectory()}/uploads";
            //    Directory.CreateDirectory(upPath);
            //    string fileName = Guid.NewGuid().ToString();

            //    using (var fileStream = new FileStream($"{upPath}/{fileName}.jpg",
            //        FileMode.Create))
            //    {
            //        await httpContext.Request.Body.CopyToAsync(fileStream);
            //    }
            //    await httpContext.Response.WriteAsync("Data saved");
            //});

            //app.MapPost("/data", async (HttpContext httpContext) =>
            //{
            //    using StreamReader streamReader = new StreamReader(
            //        httpContext.Request.Body);
            //    string mes = await streamReader.ReadToEndAsync();
            //    await httpContext.Response.WriteAsync($"Send: {mes}");

            //});

            app.MapPost("/upload", async (HttpContext httpContext) =>
            {
                IFormFileCollection formFiles = httpContext.Request.Form.Files;
                var uploadPath = $"{Directory.GetCurrentDirectory()}/uploads";
                Directory.CreateDirectory(uploadPath);

                foreach(var file in formFiles)
                {
                    string fullPath = $"{uploadPath}/{file.FileName}";
                    using(var fileSteam = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileSteam);
                    }
                }
                await httpContext.Response.WriteAsync("File saved!");
            });

            app.Run();
        }
    }
}
