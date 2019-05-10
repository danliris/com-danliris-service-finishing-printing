using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Helpers
{
    public static class ObjectExtension
    {
        public static T Clone<T>(this T objSource)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(objSource), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
