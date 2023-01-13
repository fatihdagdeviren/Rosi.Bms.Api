using Rosi.BMS.API.Core.Utilities.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosi.BMS.API.Core.Logging.Serilog
{   
    public interface IFileLogService
    {   
        public void Verbose(string message);
        public void Fatal(string message);
        public void Info(string message);
        public void Warn(string message);
        public void Debug(string message);
        public void Error(string message);
    }
}
