using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace ContactBook.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var root = config
                    .AddEnvironmentVariables()
                    .Build();
                    // Configure Azure Key Vault Connection
                    var keyVaultName = root["KeyVaultName"];
                    if (!string.IsNullOrWhiteSpace(keyVaultName))
                    {
                        var uri = "https://" + keyVaultName + ".vault.azure.net/";
                        config.AddAzureKeyVault(new Uri(uri), new DefaultAzureCredential());
                    }
                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
