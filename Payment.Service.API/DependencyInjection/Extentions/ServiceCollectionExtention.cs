using MassTransit;
using Payment.Service.API.DependencyInjection.SettingsModel;
using System.Reflection;

namespace Payment.Service.API.DependencyInjection.Extentions
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
            //services.AddDbContext<OrderContext>(options =>
            //{
            //    options.UseSqlServer(configuration.GetConnectionString("OrderMicroserviceAPI"));
            //});

            return services;
        }
    }
}
