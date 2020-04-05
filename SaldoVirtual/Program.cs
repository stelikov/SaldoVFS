using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SaldoVirtual.Services;

namespace SaldoVirtual
{
    public class Program
    {
        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true,
                             reloadOnChange: true);
            return builder.Build();
        }

        public static void Main()
        {

            var services = ConfigureServices();
            
            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<ConsoleApplication>().Run();
        }

        public static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            var config = LoadConfiguration();

            string currentPath = Directory.GetCurrentDirectory();

            if (!Directory.Exists(Path.Combine(currentPath, config.GetSection("directory").Value)))
            {
                Directory.CreateDirectory(Path.Combine(currentPath, config.GetSection("directory").Value));
            }

            // Add the config to our DI container for later user
            services.AddSingleton(config);
            services.AddScoped<ConsoleApplication>();
            services.AddScoped<IDisplayService, DisplayService>();
            services.AddScoped<IBackupService, BackupService>();
            services.AddScoped<IAttachmentFileStorage, AttachmentFileStorage>();
            services.AddScoped<IFileService, FileService>();
            return services;
        }
    }
}