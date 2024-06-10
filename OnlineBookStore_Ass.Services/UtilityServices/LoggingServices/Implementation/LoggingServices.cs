using OnlineBookStore_Ass.Domain.Entity;
using OnlineBookStore_Ass.Domain.Enums;
using OnlineBookStore_Ass.Services.LoggingServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Services.LoggingServices.Implementation
{
    public class LoggingServices:ILoggingServices
    {
        public void SaveData(Logging loggingModel)
        {
            string webRootPath = Environment.CurrentDirectory;
            string logFile = "";
            if (loggingModel.type == LogType.Succesfull)
            {
                logFile = Path.Combine(webRootPath, "Logging", "LogFiles", "Data.log");
            }
            else if (loggingModel.type == LogType.Error)
            {
                logFile = Path.Combine(webRootPath, "Logging", "LogFiles", "Error.log");
            }
            else if (loggingModel.type == LogType.Danger)
            {
                logFile = Path.Combine(webRootPath, "Logging", "LogFiles", "Danger.log");
            }
            else if (loggingModel.type == LogType.Warning)
            {
                logFile = Path.Combine(webRootPath, "Logging", "LogFiles", "Warning.log");
            }
            try
            {
                if (webRootPath != null)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(logFile));
                    using (StreamWriter writer = new StreamWriter(logFile, true))
                    {
                        writer.WriteLine($"Id:{loggingModel.id} - Controller:{loggingModel.controller} - Source:{loggingModel.source} - DateTimeNow: {loggingModel.logTime} - Log: {loggingModel.type} - Description: {loggingModel.description}");
                    }
                }
                else
                {
                    Console.WriteLine("Web root path is null.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}
