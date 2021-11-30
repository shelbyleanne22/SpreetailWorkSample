using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SpreetailWorkSample.Interfaces;
using SpreetailWorkSample.Services;
using System;

namespace SpreetailWorkSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider serviceProvider = services.BuildServiceProvider();

            MultiValueDictionaryApplication app = serviceProvider
                .GetService<MultiValueDictionaryApplication>();

            try
            {
                app.Start();
            } catch(Exception ex)
            {
                app.HandleError(ex);
            }
            finally
            {
                app.Stop();
            }
            
        }
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(configure => configure.AddConsole())
            .AddTransient<MultiValueDictionaryApplication>()
            .AddSingleton<IMultiValueDictionaryService, MultiValueDictionaryService>()
            .AddSingleton<IPrintService,PrintService>();
        }
    }
}
