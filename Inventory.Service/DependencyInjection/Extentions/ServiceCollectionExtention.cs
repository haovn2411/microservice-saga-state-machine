using Inventory.Service.API.Database;
using Inventory.Service.DependencyInjection.SettingsModel;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Cryptography;

namespace Inventory.Service.DependencyInjection.Extentions
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
                });
            });
            return services;
        }

        public static IServiceCollection AddServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });
            //DB SQL Server
            services.AddDbContext<InventoryContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("InventoryMicroServiceAPI"));
            });

            return services;
        }

    }
}
