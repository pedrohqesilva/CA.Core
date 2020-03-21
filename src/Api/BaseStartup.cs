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

        public virtual void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;

            ConfigureCultureInfo();
            RegisterContainers(services);
        }

        protected static void ConfigureCultureInfo()
        {
            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }

        protected void RegisterContainers(IServiceCollection services)
        {
            services.AddMediatR(typeof(BaseStartup));

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            Container.Register(services, connectionString);
        }
    }
}