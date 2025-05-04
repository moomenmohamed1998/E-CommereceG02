using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence.Data;

namespace E_Commerece.Wep
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region DI Contener Services

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreDBContext>(Options =>
            {
                var ConnectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");
                Options.UseSqlServer(ConnectionStrings);

            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IDbInializer, DbInializer>();
            #endregion

            var app = builder.Build();

            await InailizeDbAsync(app);


            #region MiddleWeres- Configure Pipelines

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }


        public static async Task InailizeDbAsync(WebApplication app)
        {

            using var scope = app.Services.CreateScope();
            var dbInializer = scope.ServiceProvider.GetRequiredService<IDbInializer>();
            await dbInializer.InializeAsync();
        }
    }
}
