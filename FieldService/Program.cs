using FieldServiceApp.Services;

namespace FieldServiceApp
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Field Service API",
                    Version = "v1",
                    Description = "API для работы с геопространственными данными полей"
                });
            });

            // Добавляем сервисы
            builder.Services.AddControllers();
            builder.Services.AddSingleton<IFieldService>(provider =>
                new FieldService(
                    Path.Combine(Directory.GetCurrentDirectory(), "KmlData", "centroids.kml"),
                    Path.Combine(Directory.GetCurrentDirectory(), "KmlData", "fields.kml")
                )
            );

            var app = builder.Build();

            // Настройка Swagger
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Field Service API v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
