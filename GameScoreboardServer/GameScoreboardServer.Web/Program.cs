using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace GameScoreboardServer.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).UseLightInject()
            .Build()
            .Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel();
                webBuilder.UseStartup<Startup>();
                webBuilder.UseUrls("http://::8080");
            });
    }
}
