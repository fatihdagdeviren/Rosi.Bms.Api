using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;

namespace Rosi.BMS.API.Core.Logging.Serilog
{
    public class FileLogManager : LoggerServiceBase, IFileLogService
    {
        private readonly IConfiguration _configuration;
        private readonly string folderPath;
        public FileLogManager(IConfiguration configuration)
        {
            _configuration = configuration;
            folderPath = _configuration.GetSection("SeriLogConfigurations:FileLogConfiguration:FolderPath").Value;
            var logFilePath = string.Format("{0}{1}", Directory.GetCurrentDirectory() + folderPath, ".txt");

            Logger = new LoggerConfiguration()
                .WriteTo.File(
                    logFilePath,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: null,
                    fileSizeLimitBytes: 5000000,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();            
        }

        public void Debug(string message)
        {
            Logger.Debug(message);
        }

        public void Error(string message)
        {
            Logger.Error(message);
        }

        public void Fatal(string message)
        {
            Logger.Fatal(message);
        }

        public void Info(string message)
        {
            Logger.Information(message);
        }

        public void Verbose(string message)
        {
            Logger.Verbose(message);
        }

        public void Warn(string message)
        {
            Logger.Warning(message);
        }
    }
}