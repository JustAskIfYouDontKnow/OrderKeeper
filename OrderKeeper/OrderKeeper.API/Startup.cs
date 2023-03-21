using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OrderKeeper.Database;
using OrderKeeper.Database.Service;
using OrderKeeper.Database.Service.Order;

namespace OrderKeeper.API;

public class Startup
{
    public IConfiguration Configuration { get; }


    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }


    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "OrderKeeper_Api", Version = "v1"}); });

        var typeOfContent = typeof(Startup);

        services.AddDbContext<PostgresContext>(
            options => options.UseNpgsql(
                Configuration.GetConnectionString("OrderKeeperDB"),
                b => b.MigrationsAssembly(typeOfContent.Assembly.GetName().Name)
            )
        );

        services.AddControllers();
        services.AddRazorPages();
        services.AddScoped<IDatabaseContainer, DatabaseContainer>();
        services.AddScoped<IOrderService, OrderService>();
        
        services.AddScoped<IServiceContainer, ServiceContainer>(
            provider => new ServiceContainer(
                provider.GetRequiredService<IOrderService>()
            )
        );
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PostgresContext dbContext)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderKeeper_Api v1"));
        }

        app.UseRouting();

        dbContext.Database.Migrate();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=OrderKeeper}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });
    }
    
}