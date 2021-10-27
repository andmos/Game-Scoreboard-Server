using System;
using Serilog;

namespace GameScoreboardServer.Web.Logging
{
    public class SerilogFactory : ILogFactory
    {
        private readonly Serilog.Core.Logger _logger;

        public SerilogFactory()
        {
            _logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Properties} {Message:lj}{NewLine}{Exception}{NewLine}")
                .CreateLogger();
        }

        public ILog GetLogger(Type type)
        {
            return new Log(_logger.ForContext(type).Information, _logger.ForContext(type).Debug, _logger.ForContext(type).Error);
        }
    }
}
