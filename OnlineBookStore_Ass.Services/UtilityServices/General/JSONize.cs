using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineBookStore_Ass.Services.General
{
    public class JSONize
    {
        public static string SerializeToString(object objectInstance)
        {
            return JsonSerializer.Serialize(objectInstance);
        }

        public static object DeserializeFromString(string objectData, Type objectType)
        {
            return JsonSerializer.Deserialize(objectData, objectType);
        }
    }
}
