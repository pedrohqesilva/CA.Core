using Domain.Services.EventsHandler;
using Domain.Services.Interfaces.MediatR;
using Infrastructure.CrossCutting.Bus;
using Infrastructure.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace Infrastructure.CrossCutting.IoC
{
    public static class Container
    {
        public static void Register(IServiceCollection services, string connectionString, string domainName)
        {
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<IDomainNotificationHandler, DomainNotificationHandler>();

            ConfigureDbOptions(services, connectionString, domainName);
        }

        private static void ConfigureDbOptions(IServiceCollection services, string connectionString, string domainName)
        {
            if (!string.IsNullOrEmpty(connectionString))
            {
                AddConnection(services, connectionString);
                AddTransaction(services);
                AddDbOptions(services, domainName);
            }
        }

        private static void AddConnection(IServiceCollection services, string connectionString)
        {
            services.AddScoped((serviceProvider) =>
            {
                var dbConnection = new SqlConnection(connectionString);
                dbConnection.Open();
                return dbConnection;
            });
        }

        private static void AddTransaction(IServiceCollection services)
        {
            services.AddScoped<DbTransaction>((serviceProvider) =>
            {
                var dbConnection = serviceProvider.GetService<SqlConnection>();
                return dbConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            });
        }

        private static void AddDbOptions(IServiceCollection services, string domainName)
        {
            services.AddScoped((serviceProvider) =>
            {
                var dbConnection = serviceProvider.GetService<SqlConnection>();

                var dbContext = new DbContextOptionsBuilder()
                    .UseSqlServer(dbConnection, x =>
                    {
                        x.MigrationsHistoryTable("Migrations");
                        x.MigrationsAssembly($"{domainName}.Infrastructure.Data.Migrations");
                    })
                    .EnableSensitiveDataLogging();

                AddContextLog(dbContext);
                return dbContext.Options;
            });
        }

        public static void InjectContext<TContext>(IServiceCollection services, string connectionString) where TContext : BaseContext
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                UseInMemoryDatabase<TContext>(services);
                return;
            }

            UseSqlDatabase<TContext>(services);
        }

        private static void UseInMemoryDatabase<TContext>(IServiceCollection services) where TContext : BaseContext
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            services.AddDbContext<TContext>(options => options
                .UseInternalServiceProvider(serviceProvider)
                .UseInMemoryDatabase("InMemoryDb")
                .EnableSensitiveDataLogging());
        }

        private static void UseSqlDatabase<TContext>(IServiceCollection services) where TContext : BaseContext
        {
            services.AddScoped((serviceProvider) =>
            {
                var transaction = serviceProvider.GetService<DbTransaction>();
                var options = serviceProvider.GetService<DbContextOptions>();

                var context = (TContext)Activator.CreateInstance(typeof(TContext), options);
                context.Database.UseTransaction(transaction);
                return context;
            });
        }

        private static ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging(builder =>
                builder
                    .AddConsole()
                    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information)
            );

            return serviceCollection
                .BuildServiceProvider()
                .GetService<ILoggerFactory>();
        }

        [Conditional("DEBUG")]
        private static void AddContextLog(DbContextOptionsBuilder dbContext)
        {
            dbContext.UseLoggerFactory(GetLoggerFactory());
        }
    }
}