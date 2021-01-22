using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Aspose.App.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EnableSerilog();
            CreateWebHostBuilder(args).Build().Run();
        }
        private static void EnableSerilog()
        {
            using var fs = new FileStream("appsettings.json", FileMode.Open);
            using var json = JsonDocument.Parse(fs);
            var workingDirectory = json.RootElement.GetProperty("SystemConfig").GetProperty("WorkingDirectory").GetString();
            var logFile = Path.Combine(workingDirectory, "server.log");
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Warning()
                .WriteTo.Console()
                .WriteTo.File(logFile, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseSerilog()
                .UseStartup<Startup>();
    }
}
