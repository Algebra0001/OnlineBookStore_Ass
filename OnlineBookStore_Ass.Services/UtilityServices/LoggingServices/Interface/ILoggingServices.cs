using OnlineBookStore_Ass.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Services.LoggingServices.Interface
{
    public interface ILoggingServices
    {
        void SaveData(Logging loggingModel);
    }
}
