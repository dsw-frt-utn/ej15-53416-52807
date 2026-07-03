using Dsw2026Ej15.Api.Middlewares;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Data.Sources;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.OpenApi;
using Dsw2026Ej15.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ─── DbContext ───
            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ─── Cambiar el singleton por PersistenceEf ───
            builder.Services.AddScoped<IPersistence, PersistenceEf>();

            // Add services to the container.
            builder.Services.AddControllers();
            //builder.Services.AddSingleton<IPersistence, PersistenceInMemory>();

            builder.Services.AddHealthChecks();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dsw2026Ej15 API", Version = "v1" });
            });
            
            var app = builder.Build();

            // Middleware de excepiones
            app.UseMiddleware<ExceptionMiddleware>();

            // Health Check route
            app.MapHealthChecks("/health-check");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dsw2026Ej15 API v1");
                    c.RoutePrefix = string.Empty; // ← abre Swagger en la raíz
                });
            }

            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }
    }
}
