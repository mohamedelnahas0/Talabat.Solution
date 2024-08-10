using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Extensions;
using Talabat.APIs.Middlewares;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Basket_Repository;
using Talabat.Repository.Generic_Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {



            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSwaggerServices();


            builder.Services.AddDbContext<StoreContext>(options =>
             {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnnection"));
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnnection"));
            });
            builder.Services.AddApplicationServices();

            builder.Services.AddScoped<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connectionstring = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connectionstring);
            });
            
            builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddCors(Options =>
            {
                Options.AddPolicy("MyPolicy",options =>
                {
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                    options.WithOrigins(builder.Configuration["frontUrl"]);

                });

            });

              var app = builder.Build(); 

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

        var _dbContext =services.GetRequiredService<StoreContext>(); //ask clr to create object from dbcontext explicity

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();


            try 
            {
                await _dbContext.Database.MigrateAsync(); //Updatedatabase

                var IdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();
                await IdentityDbContext.Database.MigrateAsync();
                var UserManager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsyc(UserManager);

                await StoreContextSeed.SeedAsync(_dbContext); //ApplyingDataSeed

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error has been occured during apply the migration");
                    }



            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {   
                app.UseSwaggerMiddleWare();

            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
