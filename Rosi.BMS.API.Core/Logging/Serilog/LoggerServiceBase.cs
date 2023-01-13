using Serilog;

namespace Rosi.BMS.API.Core.Logging.Serilog
{
    public abstract class LoggerServiceBase
    {
        protected ILogger Logger { get; set; }
    }
}