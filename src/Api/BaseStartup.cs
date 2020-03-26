using Infrastructure.CrossCutting.IoC;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using System.Globalization;

namespace Api
{
    public class BaseStartup
    {
        protected readonly ILogger<BaseStartup> _logger;
        protected IConfiguration Configuration { get; }

        public BaseStartup(IConfiguration configuration, ILogger<BaseStartup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        protected void BaseConfiguration(IServiceCollection services, string connectionString, string domainName)
        {
            IdentityModelEventSource.ShowPII = true;

            ConfigureCultureInfo();
            RegisterContainers(services, connectionString, domainName);
        }

        protected static void ConfigureCultureInfo()
        {
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }

        protected void RegisterContainers(IServiceCollection services, string connectionString, string domainName)
        {
            services.AddMediatR(typeof(BaseStartup));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            Container.Register(services);
        }
    }
}