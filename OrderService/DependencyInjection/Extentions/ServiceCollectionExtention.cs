using MassTransit;
using Microsoft.EntityFrameworkCore;
using Order.Service.API.Database;
using Order.Service.API.DependencyInjection.SettingsModel;
using Order.Service.API.SagaStateMachine;
using OrderService.Database;
using System.Reflection;

namespace Order.Service.API.DependencyInjection.Extentions
{
    public static class ServiceCollectionExtention
    {
        public static IServiceCollection AddConfigurationMasstransitRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var masstransitConfiguration = new MasstransitConfiguration();
            configuration.GetSection(nameof(MasstransitConfiguration)).Bind(masstransitConfiguration);

            services.AddMassTransit(opts =>
            {
                opts.AddConsumers(Assembly.GetExecutingAssembly());
                opts.AddSagaStateMachine<OrderStateMachine, OrderEntityState>()
                    .EntityFrameworkRepository(x =>
                    {
                        x.ExistingDbContext<OrderContext>();
                        x.UseSqlServer();
                    });
                opts.UsingRabbitMq((context, bus) =>
                {
                    bus.Host(masstransitConfiguration.Host, masstransitConfiguration.VHost, config =>
                    {
                        config.Username(masstransitConfiguration.UserName);
                        config.Password(masstransitConfiguration.Password);
                    });
                    bus.ConfigureEndpoints(context);
                    bus.UseInMemoryOutbox(context);
                });
            });
            return services;
        }

        public static IServiceCollection AddServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddMediatR(config =>
            //{
            //    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            //});
            //DB SQL Server
            services.AddDbContext<OrderContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("OrderMicroserviceAPI"));
            });

            return services;
        }
    }
}
