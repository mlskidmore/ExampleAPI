using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ADXETools
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.Title = "ADXETools";
                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, builder) =>
        {
            builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            builder.AddJsonFile($"appsettings.{context.HostingEnvironment}.json", optional: true);
            builder.AddEnvironmentVariables();
            if (context.HostingEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets("ADXETools");
            }
        })
        .UseStartup<Startup>()
        .Build();
    }
}
