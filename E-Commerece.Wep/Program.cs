using System.Threading.Tasks;
using Abstraction;
using Domain.Contracts;
using E_Commerece.Wep.CustomMiddlewere;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence.Data;
using Persistence.Repositories;
using Sevices;
using Shared.ErrorModelse;
using StackExchange.Redis;

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
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(AssemblyReferences).Assembly);
            builder.Services.AddScoped<IServicesManager, ServicesManager>();
            builder.Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (context) =>
                {
                    var Errors = context.ModelState.Where(M => M.Value.Errors.Any())
                    .Select(M => new ValidayionError()
                    {
                        Field = M.Key,
                        Errors = M.Value.Errors.Select(E => E.ErrorMessage).ToArray()
                    });
                    var Response = new ValidationErrorToRuturn()
                    {
                        ValidtionErrors = Errors,
                    };
                    return new BadRequestObjectResult(Response);
                };
            });

            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnectionString"));
            });

            #endregion

            var app = builder.Build();

            await InailizeDbAsync(app);


            #region MiddleWeres- Configure Pipelines
            app.UseMiddleware<CustomExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

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
