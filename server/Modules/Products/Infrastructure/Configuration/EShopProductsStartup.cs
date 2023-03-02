﻿using Autofac;
using eShopModular.Common.Application;
using eShopModular.Modules.Products.Infrastructure.Configuration.DataAccess;
using eShopModular.Modules.Products.Infrastructure.Configuration.Logging;
using eShopModular.Modules.Products.Infrastructure.Configuration.Mediation;
using Serilog.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace eShopModular.Modules.Products.Infrastructure.Configuration
{
    public class EShopProductsStartup
    {
        private static IContainer? _container;

        public static void Initialize(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger)
        {
            var moduleLogger = logger.ForContext("Module", "EShopOrders");

            ConfigureCompositionRoot(
                connectionString,
                executionContextAccessor,
                moduleLogger);
        }

        public static void Stop()
        {
        }

        private static void ConfigureCompositionRoot(
            string connectionString,
            IExecutionContextAccessor executionContextAccessor,
            ILogger logger)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger.ForContext("Module", "EShopOrders")));

            var loggerFactory = new SerilogLoggerFactory(logger);

            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));
            containerBuilder.RegisterModule(new MediatorModule());

            containerBuilder.RegisterInstance(executionContextAccessor);

            _container = containerBuilder.Build();

            EShopProductsCompositionRoot.SetContainer(_container);
        }
    }
}