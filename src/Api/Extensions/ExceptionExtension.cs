using Api.Exceptions;
using GlobalExceptionHandler.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Api.Extensions
{
    public static class ExceptionConfigurationExtension
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder app, ILogger<BaseStartup> logger)
        {
            app.UseGlobalExceptionHandler(configuration => ExceptionConfiguration(configuration, logger));
        }

        private static void ExceptionConfiguration(ExceptionHandlerConfiguration configuration, ILogger<BaseStartup> logger)
        {
            configuration.ContentType = "application/problem+json";

            ConfigureDebugInternalError(configuration, logger);
            ConfigureReleaseInternalError(configuration, logger);
            ConfigureOnError(configuration, logger);
        }

        [Conditional("DEBUG")]
        private static void ConfigureDebugInternalError(ExceptionHandlerConfiguration configuration, ILogger<BaseStartup> logger)
        {
            configuration.ResponseBody(s =>
            {
                logger.LogError(s.ToString());
                return JsonConvert.SerializeObject(new ExeceptionResponse(s.Message, s.StackTrace, s.InnerException?.Message));
            });
        }

        [Conditional("RELEASE")]
        private static void ConfigureReleaseInternalError(ExceptionHandlerConfiguration configuration, ILogger<BaseStartup> logger)
        {
            configuration.ResponseBody(s =>
            {
                logger.LogError(s.ToString());
                return JsonConvert.SerializeObject(new ExeceptionResponse("Ocorreu um erro interno não tratado."));
            });
        }

        private static void ConfigureOnError(ExceptionHandlerConfiguration configuration, ILogger<BaseStartup> logger)
        {
            configuration.OnError((exception, httpContext) =>
            {
                var error = new ExeceptionResponse(exception.Message, exception.StackTrace, exception.InnerException?.Message);
                logger.LogError(error.ToString());
                return Task.CompletedTask;
            });
        }
    }
}