using Microsoft.Extensions.DependencyInjection;
using ProjQuakeLogParser.APPLICATION.Intefaces;
using ProjQuakeLogParser.APPLICATION.Services;

namespace ProjQuakeLogParser.IOC.Services
{
    public static class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IQuakeLogParser, QuakeLogParserService>();
        }
    }
}
