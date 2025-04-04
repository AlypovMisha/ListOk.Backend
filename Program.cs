using ListOk.Application.Services;
using ListOk.Core.Interfaces;
using ListOk.Core.Interfaces.Infrastucture;
using ListOk.Infrastructure.Persistent;
using ListOk.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ListOk
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationDbContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IBoardRepository, BoardRepository>();
            builder.Services.AddScoped<IBoardService, BoardService>();
            builder.Services.AddScoped<IColumnRepository, ColumnRepository>();
            builder.Services.AddScoped<IColumnService, ColumnService>();
            builder.Services.AddScoped<ICardRepository, CardRepository>();
            builder.Services.AddScoped<ICardService, CardService>();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using var scope = app.Services.CreateScope();
            await using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.EnsureCreatedAsync();


            app.UseHttpsRedirection();

            app.UseCors("AllowAll");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
