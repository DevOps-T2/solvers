using Contexts.Solvers;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Solvers.App.Actions;
using Solvers.App.Serializers;
using System.Reflection;

namespace Solvers.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //var bus = RabbitHutch.CreateBus(Configuration.GetConnectionString("RabbitMq"), c => c.Register<ISerializer, ProtobufSerializer>());

            //services.AddSingleton(bus.PubSub);
            //services.AddSingleton(bus);


            //services.AddScoped<CreateSolver>();
            services.AddScoped<Logging>();
            services.AddRazorPages();
            services.AddControllersWithViews();

            /*services.AddDbContext<AppDbContext>(options => options
            .UseMySql(Configuration.GetConnectionString("Database"), new MySqlServerVersion(new Version(8, 0, 27)))

            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors());
            */
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Solvers Service", Description = "Documentation for the solver service.", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            // Configure the HTTP request pipeline.
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            /*
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        
            using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if(context.Database.IsMySql())
            {
                context.Database.Migrate();
            }*/

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.Map("api", () => "Solvers API");
                endpoints.MapControllerRoute(
                    name: "solvers",
                    pattern: "api/{controller=Solvers}/{action=Index}/{id?}");
            });

            /*var subscriber = new AutoSubscriber(app.ApplicationServices.GetRequiredService<IBus>(), "solvers")
            {
                AutoSubscriberMessageDispatcher = new ScopeDispatcher(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>())
            };

            subscriber.Subscribe(Assembly.GetExecutingAssembly().GetTypes());
            subscriber.SubscribeAsync(Assembly.GetExecutingAssembly().GetTypes()); */

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Solvers API");
            });

        }
    }

    public class ScopeDispatcher : IAutoSubscriberMessageDispatcher
    {
        private readonly IServiceScopeFactory _factory;

        public ScopeDispatcher(IServiceScopeFactory factory)
        {
            _factory = factory;
        }
        void IAutoSubscriberMessageDispatcher.Dispatch<TMessage, TConsumer>(TMessage message, CancellationToken cancellationToken)
        {
            using var scope = _factory.CreateScope();

            var consumer = scope.ServiceProvider.GetRequiredService<TConsumer>();

            consumer.Consume(message, cancellationToken);
        }

        async Task IAutoSubscriberMessageDispatcher.DispatchAsync<TMessage, TConsumer>(TMessage message, CancellationToken cancellationToken)
        {
            using var scope = _factory.CreateAsyncScope();

            var consumer = scope.ServiceProvider.GetRequiredService<TConsumer>();

            await consumer.ConsumeAsync(message, cancellationToken);
        }
    }
}
