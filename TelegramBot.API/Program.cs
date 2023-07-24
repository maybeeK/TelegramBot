using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using TelegramBot.API.BackgroundServices;
using TelegramBot.API.Data;
using TelegramBot.API.Hubs;
using TelegramBot.API.Services;
using TelegramBot.API.Services.Interfaces;

namespace TelegramBot.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("DbConnection") ?? throw new InvalidOperationException("Connection string 'DbConnection' not found.");
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            builder.Services.AddHostedService<CoursesBackgroundService>();

            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<IUserTagService, UserTagService>();
            builder.Services.AddSingleton<CourseHub>();
            builder.Services.AddSignalR();

            builder.Services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults
                                    .MimeTypes
                                    .Concat(new[] { "application/octet-stream" });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseResponseCompression();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.MapHub<CourseHub>("/coursehub");

            app.Run();
        }
    }
}