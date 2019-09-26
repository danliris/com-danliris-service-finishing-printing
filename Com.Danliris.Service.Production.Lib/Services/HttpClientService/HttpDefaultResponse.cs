using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Services.HttpClientService
{
    public class HttpDefaultResponse<T>
    {
        public string apiVersion { get; set; }
        public T data { get; set; }
        public string message { get; set; }
        public int statusCode { get; set; }
    }
}
