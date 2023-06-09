using Chaturgate.Data;
using Microsoft.EntityFrameworkCore;
using Chaturgate.WebApi.Extenstions;
using Chaturgate.Data.Seeder;

namespace Chaturgate.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services, builder.Configuration);
            var app = builder.Build();
            Configure(app);
            app.Run();
        }

        // use service collection extensions to configure services here
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = services.GetApplicationSettings(configuration);

            services
                .AddDatabase(configuration)
                .AddIdentity()
                .AddJwtAuthentication(appSettings)
                .AddApplicationServices()
                .AddSwagger();

            services.AddControllers();

            services.AddSingleton(configuration);
            services.AddSignalR();
        }

        private static void Configure(WebApplication app)
        {
            // Seed data on application startup
            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ChaturgateDbContext>();
                dbContext.Database.Migrate();
                new ChaturgateDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.MapControllers();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}