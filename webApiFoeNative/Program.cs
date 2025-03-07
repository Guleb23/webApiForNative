
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using webApiFoeNative.Database;
using webApiFoeNative.Models;

namespace webApiFoeNative
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();






            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            // Проверяем, существует ли папка, и создаем её, если нет
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }


            var app = builder.Build();
            app.UseCors("AllowAllOrigins");
            app.UseRouting();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")),
                RequestPath = "/uploads"
            });


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }











            app.MapGet("/spec", (ApplicationDBContext ctx) =>
            {

                return ctx.Specialisation.ToList(); ;
            });
            app.MapGet("/docators", (ApplicationDBContext ctx) =>
            {

                return ctx.Doctors.ToList(); ;
            });




            app.MapPost("/docreg", async (HttpContext context, ApplicationDBContext db) =>
            {
                if (!context.Request.HasFormContentType)
                {
                    return Results.BadRequest("Ожидается форма с файлом.");
                }
                var form = await context.Request.ReadFormAsync();
                var file = form.Files["photo"];
                var Name = form["Name"];
                var Phone = form["Phone"];
                var Password = form["Password"];
                var Stash = form["Stash"];
                var Description = form["Description"];
                var CategoryId = form["CategoryId"];
                Console.WriteLine(form);

                if (file == null)
                {
                    return Results.BadRequest("Файл обязательны.");
                }

                // Генерация уникального имени файла
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                // Сохранение файла на сервере
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Сохранение информации в базу данных
                DoctorsModel doc  = new DoctorsModel
                {
                    Name = Name,
                    Phone = Phone,
                    Password = Password,
                    Stash = int.Parse(Stash),
                    CategoryId = int.Parse(CategoryId),
                    Description = Description,
                    ImagePath = $"/uploads/{fileName}"
                };

                db.Doctors.Add(doc);
                await db.SaveChangesAsync();

                return Results.Ok(doc);
            });

            app.Run();


            app.Run();
        }
    }
}
