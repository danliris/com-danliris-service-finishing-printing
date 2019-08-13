using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.OrderStatusReport;
using Com.Danliris.Service.Finishing.Printing.Lib.Services.HttpClientService;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.OrderStatusReports;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Test.Services.OrderStatusReportServiceTest
{
    public class HttpResponseTestGetYearly : IHttpClientService
    {
        //private readonly HttpClient _client = new HttpClient();

        //public HttpResponseTestGetYearly(IIdentityService identityService)
        //{
        //    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, identityService.Token);
        //    _client.DefaultRequestHeaders.Add("x-timezone-offset", identityService.TimezoneOffset.ToString());
        //}

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return Task.Run(() => new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(new HttpDefaultResponse<List<YearlyOrderQuantity>>()
                {
                    apiVersion = "1.0",
                    data = new List<YearlyOrderQuantity>()
                    {
                        new YearlyOrderQuantity()
                        {
                            Month = 1,
                            OrderIds = new List<int>() { 0 },
                            OrderQuantity = 10
                        }
                    },
                    message = "message",
                    statusCode = 200
                }))
            });
        }

        public Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PutAsync(string url, HttpContent content)
        {
            throw new NotImplementedException();
        }
    }

    public class HttpResponseTestGetMonthly : IHttpClientService
    {
        //private readonly HttpClient _client = new HttpClient();

        //public HttpResponseTestGetYearly(IIdentityService identityService)
        //{
        //    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, identityService.Token);
        //    _client.DefaultRequestHeaders.Add("x-timezone-offset", identityService.TimezoneOffset.ToString());
        //}

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return Task.Run(() => new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(new HttpDefaultResponse<List<MonthlyOrderQuantity>>()
                {
                    apiVersion = "1.0",
                    data = new List<MonthlyOrderQuantity>()
                    {
                        new MonthlyOrderQuantity()
                        {
                            orderId = 0,
                            orderQuantity = 10
                        }
                    },
                    message = "message",
                    statusCode = 200
                }))
            });
        }

        public Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> PutAsync(string url, HttpContent content)
        {
            throw new NotImplementedException();
        }
    }
}
